using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO
{
    public class ReportsOperations
    {
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();

        public ReportsApiResponse GetReports(GetReportViewModel GRVM)
        {

            List<ReportModel> RCL = new List<ReportModel>();
            DriverReportHeader DRH = new DriverReportHeader();
            int totalhouses = 0;
           



                string query = @"SELECT
    COALESCE(A.DateTime, B.DateTime, C.DateTime, D.DateTime) AS DateTime,
   
    A.WetWasteCollected,
    A.WetWasteReceived,
    A.WetWasteProcessed,
    A.WetWasteRecovery,    
    B.DryWasteCollected,
    B.DryWasteReceived,
    B.DryWasteProcessed,
    B.DryWasteRecovery,
    C.HHWasteCollected,
    C.HHHSafelyDisposed,
    D.MixedWasteCollected,
    D.MixedWasteDisposed,
    D.MixedWasteRecovery,
	COALESCE(A.HousesCollected, B.HousesCollected, C.HousesCollected, D.HousesCollected) AS HousesCollected    
	--COALESCE(A.Trips, B.Trips, C.Trips, D.Trips) AS Trips
FROM
    (SELECT
        CONVERT(DATE, datetime) AS DateTime,
        
        SUM(wetwastecollected) AS WetWasteCollected,
        SUM(wetwastereceived) AS WetWasteReceived,
        SUM(wetwasteprocessed) AS WetWasteProcessed,
        SUM(wetwasterecovery) AS WetWasteRecovery,
        MAX(HousesCollected) AS HousesCollected
       -- COUNT(*) AS Trips
    FROM
        TotalWetWastage
    WHERE
        CONVERT(DATE, datetime) BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + @"'
    GROUP BY
        CONVERT(DATE, datetime)
        ) A

FULL OUTER JOIN
    (SELECT
        CONVERT(DATE, datetime) AS DateTime,
        
        MAX(HousesCollected) AS HousesCollected,
       -- COUNT(*) AS Trips,
        SUM(drywastecollected) AS DryWasteCollected,
        SUM(drywastereceived) AS DryWasteReceived,
        SUM(drywasteprocessed) AS DryWasteProcessed,
        SUM(drywasterecovery) AS DryWasteRecovery
    FROM
        TotalDryWastage
    WHERE
        CONVERT(DATE, datetime) BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + @"'
    GROUP BY
        CONVERT(DATE, datetime)
        ) B

ON A.DateTime = B.DateTime 

FULL OUTER JOIN
    (SELECT
        CONVERT(DATE, datetime) AS DateTime,
        
        MAX(HousesCollected) AS HousesCollected,
       -- COUNT(*) AS Trips,
        SUM(hhwastecollected) AS HHWasteCollected,
		SUM(TotalHHHSafelyDisposed) AS HHHSafelyDisposed
    FROM
        TotalHHWastage
    WHERE
        CONVERT(DATE, datetime) BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + @"'
    GROUP BY
        CONVERT(DATE, datetime)
        ) C

ON A.DateTime = C.DateTime 

FULL OUTER JOIN
    (SELECT
        CONVERT(DATE, datetime) AS DateTime,
        
        MAX(HousesCollected) AS HousesCollected,
      --  COUNT(*) AS Trips,
         SUM(mixedwastecollected) AS MixedWasteCollected,
        SUM(TotalMixedWasteDisposed) AS MixedWasteDisposed,
        SUM(mixedwasterecovery) AS MixedWasteRecovery
    FROM
        TotalMixedWastage
    WHERE
        CONVERT(DATE, datetime) BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + @"'
    GROUP BY
        CONVERT(DATE, datetime)
        ) D

ON A.DateTime = D.DateTime
";


                DataTable queryResult = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(queryResult);
                    }
                    string sumQuery = "select COUNT(*) from HouseHold as TotalHouses;";

                    using (SqlCommand sumCommand = new SqlCommand(sumQuery, connection))
                    {
                        totalhouses = Convert.ToInt32(sumCommand.ExecuteScalar());
                    }
                    string CompostAndRecoveryQuery = @"
    SELECT 
        COALESCE(SUM(CASE WHEN WasteType = 'wet' THEN Recovery END), 0) AS Compost,
        COALESCE(SUM(CASE WHEN WasteType = 'dry' THEN Recovery END), 0) AS SentForRecycling
    FROM (
        SELECT 'wet' AS WasteType, wetwasterecovery AS Recovery FROM totalwetwastage  WHERE CONVERT(DATE, datetime) BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + @"'
        UNION ALL
        SELECT 'dry' AS WasteType, drywasterecovery AS Recovery FROM totaldrywastage  WHERE CONVERT(DATE, datetime) BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + @"'
    ) AS subquery;";

                    decimal compost = 0;
                    decimal sentforrecycling = 0;

                    using (SqlCommand compostAndRecoveryCommand = new SqlCommand(CompostAndRecoveryQuery, connection))
                    {
                        using (SqlDataReader reader = compostAndRecoveryCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                compost = Convert.ToDecimal(reader["Compost"]);
                                sentforrecycling = Convert.ToDecimal(reader["SentForRecycling"]);
                            }
                        }
                    }
                    int Trips = 0;

                    string tripcount = @"SELECT COUNT(*) AS NoOfTrips FROM Trips WHERE DateandTime BETWEEN '" + GRVM.From.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + GRVM.To.ToString("yyyy-MM-dd 23:59:59") + "' AND CountTrueOrFalse = 1";

                    using (SqlCommand command = new SqlCommand(tripcount, connection))
                    {
                        //connection.Open();

                        // Execute the query and store the result in tripCountResult
                        object result = command.ExecuteScalar();

                        // Check if the result is not null and can be converted to an int
                        if (result != null && int.TryParse(result.ToString(), out Trips))
                        {
                            // Successfully converted to int, now tripCountResult holds the count
                        }
                        else
                        {
                            // Handle the case where the result couldn't be converted to int
                            // Maybe log an error or set a default value for tripCountResult
                        }
                    }

                    foreach (DataRow row in queryResult.Rows)
                    {
                        ReportModel wasteCollection = new ReportModel
                        {
                            DateTime = Convert.ToDateTime(row["DateTime"]).Date,
                            //VehicleNumber = Convert.ToString(row["vehiclenumber"]),
                            WetWasteCollected = Convert.ToDecimal(row["WetWasteCollected"]),
                            WetWasteProcessed = Convert.ToDecimal(row["WetWasteProcessed"]),
                            DryWasteCollected = Convert.ToDecimal(row["DryWasteCollected"]),
                            DryWasteProcessed = Convert.ToDecimal(row["DryWasteProcessed"]),
                            HHWasteCollected = Convert.ToDecimal(row["HHWasteCollected"]),
                            HHHSafelyDisposed = Convert.ToDecimal(row["HHHSafelyDisposed"]),
                            MixedWasteCollected = Convert.ToDecimal(row["MixedWasteCollected"]),
                            MixedWasteDisposed = Convert.ToDecimal(row["MixedWasteDisposed"]),
                            Compost = compost,
                            Recycled = sentforrecycling,
                            HousesCollected = Convert.ToInt32(row["HousesCollected"]),
                            HousesCount = totalhouses,
                            // Trips = Convert.ToInt16(row["Trips"])
                            Trips = Trips
                        };

                        RCL.Add(wasteCollection);

                    }
                return new ReportsApiResponse
                {
                    ReportHeader = DRH,
                    ReportList = RCL
                };
            }
            }
            }
    
}
