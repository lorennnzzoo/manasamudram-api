using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO
{
    public class TripOperations
    {
        private ManasamudramEntities context = new ManasamudramEntities();
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();

        public string GetTripCount(string UserName)
        {
            DateTime currentDate = DateTime.Today;

            string queryCount = "SELECT TOP 1 NoOfTrips FROM Trips WHERE DriveName = @DriverName AND DateandTime = @CurrentDate ORDER BY NoOfTrips DESC;";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand commandCount = new SqlCommand(queryCount, connection))
                {
                    commandCount.Parameters.AddWithValue("@DriverName", UserName);
                    commandCount.Parameters.AddWithValue("@CurrentDate", currentDate.ToString("yyyy-MM-dd"));

                    connection.Open();
                    var result = commandCount.ExecuteScalar();
                    
                    if (result != null)
                    {
                        return Convert.ToString( result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        public int GetTripCount()
        {
            int tripcount = 0;
            bool CountTrueOrFalse = false;
            DateTime currentdate = DateTime.Today;

            string query = "SELECT NoOfTrips FROM Trips WHERE DateandTime = @CurrentDate ORDER BY NoOfTrips DESC";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CurrentDate", currentdate);

                    
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        
                        tripcount = Convert.ToInt32(result) + 1;
                    }
                    else
                    {
                        
                        tripcount = 1;
                    }

                    string query1 = "INSERT INTO Trips (NoOfTrips, DateandTime, CountTrueOrFalse) VALUES (@tripcount, @currentdate, @CountTrueOrFalse)";

                    using (SqlCommand insertCommand = new SqlCommand(query1, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@tripcount", tripcount);
                        insertCommand.Parameters.AddWithValue("@currentdate", currentdate);
                        insertCommand.Parameters.AddWithValue("@CountTrueOrFalse", CountTrueOrFalse);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }

            return tripcount;
        }



        public void UpdateTripDetails(string TripCount, string VehicleNumber, int NoofWorkers, string driverName)
        {
            DateTime currentdate = DateTime.Today;
            string vehicleNumberPattern = "^[A-Z]{2}\\d{2}[A-Z]{2}\\d{4}$";

            if (NoofWorkers < 1 || NoofWorkers > 999)
            {             
                throw new ArgumentException("NoofWorkers should be greater than or equal to 1");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(VehicleNumber, vehicleNumberPattern))
            {                                
                throw new ArgumentException("Invalid Vehicle Number format");
            }

            bool CountTrueOrFalse = true;
            string[] parts = TripCount.Split('-');

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid formattedResult format");
            }

            string tripNumber = parts[1].Trim();

            string query = "INSERT INTO EndScanning(StartScanning, EndScanning, Drivename, Date, Tripcount) VALUES('1', '0', '" + driverName + "', '" + currentdate.ToString("yyyy-MM-dd") + "','" + tripNumber + "');";

            string query1 = "UPDATE Trips SET NoofWorkers = '" + NoofWorkers + "', DriveName = '" + driverName + "', CountTrueOrFalse = '" + CountTrueOrFalse + "', VehicleNumber = '" + VehicleNumber + "' " +
                           "WHERE DateandTime = '" + currentdate.ToString("yyyy-MM-dd") + "' AND NoOfTrips = '" + tripNumber + "'";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }                
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
