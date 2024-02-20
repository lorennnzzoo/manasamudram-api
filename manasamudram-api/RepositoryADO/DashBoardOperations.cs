using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Configuration;

namespace RepositoryADO
{
    public class DashBoardOperations
    {
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();
        public DashBoard GetDashBoardData(DashBoardViewModel DBVM)
        {
            string formattedStartFrom = ConfigurationManager.AppSettings["DashboardStartdate"];
            DateTime formattedFrom = DateTime.Parse(formattedStartFrom);
            string From = formattedFrom.ToString("yyyy-MM-dd 00:00:00");

            DateTime formattedTo = DateTime.Now;
            string To = formattedTo.ToString("yyyy-MM-dd 23:59:59");




            if (DBVM.From != null)
            {
                formattedFrom = DBVM.From.Value;
                From = formattedFrom.ToString("yyyy-MM-dd 00:00:00");
                formattedTo = DBVM.To.Value;
                To = formattedTo.ToString("yyyy-MM-dd 23:59:59");
            }

            decimal percapitawastegeneration = 0;
            decimal serviceEfficiency = 0;
            decimal wetWasteProcessingEfficiency = 0;
            decimal dryWasteProcessingEfficiency = 0;
            decimal hhwasteProcessingEfficiency = 0;
            decimal mixedWasteProcessingEfficiency = 0;
            decimal wetWasteCollected = 0;
            decimal wetWasteProcessed = 0;
            decimal dryWasteCollected = 0;
            decimal dryWasteProcessed = 0;
            decimal hazardousWasteCollected = 0;
            decimal hazardousWasteProcessed = 0;
            decimal mixedWasteCollected = 0;
            decimal mixedWasteProcessed = 0;
            decimal compost = 0;
            decimal sentforrecycling = 0;
            decimal visitedhouses = 0;
           
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();

                    string wasteQuery = @"
                    SELECT 
    CAST(
        SUM(CAST(COALESCE(wet_waste, 0) AS DECIMAL(10, 2))) +
        SUM(CAST(COALESCE(dry_waste, 0) AS DECIMAL(10, 2))) +
        SUM(CAST(COALESCE(hazardous_waste, 0) AS DECIMAL(10, 2))) +
        SUM(CAST(COALESCE(mixed_waste, 0) AS DECIMAL(10, 2)))
    AS DECIMAL(10, 2)) AS total_result
FROM (
    SELECT 
        (SELECT SUM(wetwastecollected) FROM totalwetwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"') AS wet_waste,
        (SELECT SUM(drywastecollected) FROM totaldrywastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"') AS dry_waste,
        (SELECT SUM(hhwastecollected) FROM totalhhwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"') AS hazardous_waste,
        (SELECT SUM(mixedwastecollected) FROM totalmixedwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"') AS mixed_waste
) AS subquery;;";

                    using (SqlCommand wasteCommand = new SqlCommand(wasteQuery, connection))
                    {
                        decimal totalWaste = Convert.ToDecimal(wasteCommand.ExecuteScalar());

                        // string sumQuery = "SELECT SUM(CAST(NumberOfPersons AS INT)) AS TotalPersons FROM HouseHold;";
                        string sumQuery = @"select count(distinct WastageConfirmation.HouseId) as visitedhouses from WastageConfirmation where Datetime BETWEEN '" + From + "' AND '" + To + @"'";
                        using (SqlCommand sumCommand = new SqlCommand(sumQuery, connection))
                        {
                            int totalPersons = Convert.ToInt32(sumCommand.ExecuteScalar());



                            int numberOfDays = (int)(formattedTo - formattedFrom).TotalDays;

                            percapitawastegeneration = (numberOfDays * totalPersons > 0) ? totalWaste / (numberOfDays * totalPersons) : 0;

                        }
                        string visitedhousesquery = @"select count(distinct WastageConfirmation.HouseId) as visitedhouses from WastageConfirmation where Datetime BETWEEN '" + From + "' AND '" + To + @"'";

                        using (SqlCommand serviceEfficiencyCommand = new SqlCommand(visitedhousesquery, connection))
                        {
                            visitedhouses = Convert.ToInt32(serviceEfficiencyCommand.ExecuteScalar());
                        }
                        string noofhousesquery = @"SELECT COUNT(DISTINCT HouseHold.ID) as noofhouses from HouseHold ";
                        using (SqlCommand serviceEfficiencyCommand2 = new SqlCommand(noofhousesquery, connection))
                        {
                            decimal noofhouses = Convert.ToInt32(serviceEfficiencyCommand2.ExecuteScalar());
                            serviceEfficiency = (visitedhouses / noofhouses) * 100;
                        }

                        string wasteProcessingEfficiencyQuery = @"SELECT 
         
		      CAST((SUM(CAST(CASE WHEN WasteType = 'wet' THEN Recovery END AS DECIMAL(10, 2))) / NULLIF(SUM(CAST(CASE WHEN WasteType = 'wet' THEN Processed END AS DECIMAL(10, 2))), 0)) * 100 AS DECIMAL(10, 2)) AS wetwasteprocessingefficiency,
			  CAST((SUM(CAST(CASE WHEN WasteType = 'dry' THEN Recovery END AS DECIMAL(10, 2))) / NULLIF(SUM(CAST(CASE WHEN WasteType = 'dry' THEN Processed END AS DECIMAL(10, 2))), 0)) * 100 AS DECIMAL(10, 2)) AS drywasteprocessingefficiency,
			  CAST((SUM(CAST(CASE WHEN WasteType = 'hhw' THEN Recovery END AS DECIMAL(10, 2))) / NULLIF(SUM(CAST(CASE WHEN WasteType = 'hhw' THEN Processed END AS DECIMAL(10, 2))), 0)) * 100 AS DECIMAL(10, 2)) AS hhwasteprocessingefficiency,
			  CAST((SUM(CAST(CASE WHEN WasteType = 'mixed' THEN Recovery END AS DECIMAL(10, 2))) / NULLIF(SUM(CAST(CASE WHEN WasteType = 'mixed' THEN Processed END AS DECIMAL(10, 2))), 0)) * 100 AS DECIMAL(10, 2)) AS mixedwasteprocessingefficiency
			   FROM (
                SELECT 'wet' AS WasteType, wetwasteprocessed AS Processed, wetwasterecovery AS Recovery FROM totalwetwastage WHERE datetime BETWEEN '" + From + "' AND '" + To + @"'
                UNION ALL
                SELECT 'dry' AS WasteType, drywasteprocessed AS Processed, drywasterecovery AS Recovery FROM totaldrywastage WHERE datetime BETWEEN '" + From + "' AND '" + To + @"'
                UNION ALL
                SELECT 'hhw' AS WasteType, HHWasteCollected AS Processed, TotalHHHSafelyDisposed AS Recovery FROM totalhhwastage WHERE datetime BETWEEN '" + From + "' AND '" + To + @"'
                UNION ALL
                SELECT 'mixed' AS WasteType, TotalMixedWasteDisposed AS Processed, mixedwasterecovery AS Recovery FROM totalmixedwastage WHERE datetime BETWEEN '" + From + "' AND '" + To + @"'
            ) AS subquery;";
                        using (SqlCommand wasteProcessingEfficiencyCommand = new SqlCommand(wasteProcessingEfficiencyQuery, connection))
                        {
                            using (SqlDataReader reader = wasteProcessingEfficiencyCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    wetWasteProcessingEfficiency = Convert.ToDecimal(reader["wetwasteprocessingefficiency"]);
                                    dryWasteProcessingEfficiency = Convert.ToDecimal(reader["drywasteprocessingefficiency"]);
                                    hhwasteProcessingEfficiency = Convert.ToDecimal(reader["hhwasteprocessingefficiency"]);
                                    mixedWasteProcessingEfficiency = Convert.ToDecimal(reader["mixedwasteprocessingefficiency"]);
                                }
                            }
                        }

                        string collectionAndProcessedquery = @"
           SELECT 
    CAST(
        COALESCE((SELECT SUM(wetwastecollected) FROM totalwetwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS wet_waste_collected,
    CAST(
        COALESCE((SELECT SUM(wetwasteprocessed) FROM totalwetwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS wet_waste_processed,
    
    CAST(
        COALESCE((SELECT SUM(drywastecollected) FROM totaldrywastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS dry_waste_collected,
    CAST(
        COALESCE((SELECT SUM(drywasteprocessed) FROM totaldrywastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS dry_waste_processed,
    
    CAST(
        COALESCE((SELECT SUM(hhwastecollected) FROM totalhhwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS hazardous_waste_collected,
    CAST(
        COALESCE((SELECT SUM(TotalHHHSafelyDisposed) FROM totalhhwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS hazardous_waste_processed,
    
    CAST(
        COALESCE((SELECT SUM(mixedwastecollected) FROM totalmixedwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS mixed_waste_collected,
    CAST(
        COALESCE((SELECT SUM(TotalMixedWasteDisposed) FROM totalmixedwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'), 0) 
        AS DECIMAL(10, 2)) AS mixed_waste_processed;
;";

                        using (SqlCommand collectionAndProcessed = new SqlCommand(collectionAndProcessedquery, connection))
                        {
                            using (SqlDataReader reader = collectionAndProcessed.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    wetWasteCollected = Convert.ToDecimal(reader["wet_waste_collected"]);
                                    wetWasteProcessed = Convert.ToDecimal(reader["wet_waste_processed"]);
                                    dryWasteCollected = Convert.ToDecimal(reader["dry_waste_collected"]);
                                    dryWasteProcessed = Convert.ToDecimal(reader["dry_waste_processed"]);
                                    hazardousWasteCollected = Convert.ToDecimal(reader["hazardous_waste_collected"]);
                                    hazardousWasteProcessed = Convert.ToDecimal(reader["hazardous_waste_processed"]);
                                    mixedWasteCollected = Convert.ToDecimal(reader["mixed_waste_collected"]);
                                    mixedWasteProcessed = Convert.ToDecimal(reader["mixed_waste_processed"]);
                                }
                            }
                        }

                        string CompostAndRecovery = @"
            SELECT 
                COALESCE(SUM(CASE WHEN WasteType = 'wet' THEN Recovery END), 0) AS Compost,
                COALESCE(SUM(CASE WHEN WasteType = 'dry' THEN Recovery END), 0) AS SentForRecycling
            FROM (
                SELECT 'wet' AS WasteType, wetwasterecovery AS Recovery FROM totalwetwastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'
                UNION ALL
                SELECT 'dry' AS WasteType, drywasterecovery AS Recovery FROM totaldrywastage WHERE Datetime BETWEEN '" + From + "' AND '" + To + @"'
            ) AS subquery;";

                        using (SqlCommand command = new SqlCommand(CompostAndRecovery, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    compost = Convert.ToDecimal(reader["Compost"]);
                                    sentforrecycling = Convert.ToDecimal(reader["SentForRecycling"]);
                                }
                            }
                        }
                    }
                }
                DashBoard db = new Models.DashBoard
                {
                    PerCapitaWasteGeneration = percapitawastegeneration,
                    ServiceEfficiency = serviceEfficiency,
                    WetWasteProcessingEfficiency = wetWasteProcessingEfficiency,
                    DryWasteProcessingEfficiency = dryWasteProcessingEfficiency,
                    HHWasteProcessingEfficiency = hhwasteProcessingEfficiency,
                    MixedWasteProcessingEfficiency = mixedWasteProcessingEfficiency,
                    TotalWetWasteCollected = wetWasteCollected,
                    TotalDryWasteCollected = dryWasteCollected,
                    TotalHHWasteCollected = hazardousWasteCollected,
                    TotalMixedWasteCollected = mixedWasteCollected,
                    TotalWetWasteProcessed = wetWasteProcessed,
                    TotalDryWasteProcessed = dryWasteProcessed,
                    TotalHHWasteProcessed = hazardousWasteProcessed,
                    TotalMixedWasteProcessed = mixedWasteProcessed,
                    CompostProduced = compost,
                    SentForRecycling = sentforrecycling
                };
            return db;
            }
    }
}
