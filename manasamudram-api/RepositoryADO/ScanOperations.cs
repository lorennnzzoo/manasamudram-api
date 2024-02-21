using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryADO
{
    public class ScanOperations
    {
        private ManasamudramEntities context = new ManasamudramEntities();
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();

        public string InsertEndScan(string driverName, string tripNo)
        {
            DateTime currentDate = DateTime.Today;

            string queryCount = "SELECT COUNT(ServiceGiven) FROM WastageConfirmation WHERE Drivername = @DriverName AND CAST(Datetime AS DATE) = @CurrentDate AND IsScannestatus = 1 AND Trip = '" + tripNo + "'";
            string queryUpdate = "UPDATE EndScanning SET Endscanning = 1 WHERE DriveName = @DriverName AND Date = @CurrentDate AND Startscanning = 1 ";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand commandCount = new SqlCommand(queryCount, connection))
                {
                    commandCount.Parameters.AddWithValue("@DriverName", driverName);
                    commandCount.Parameters.AddWithValue("@CurrentDate", currentDate);

                    try
                    {
                        connection.Open();
                        int rowCount = (int)commandCount.ExecuteScalar();

                        if (rowCount == 0)
                        {
                            return "Success: No records found in WastageConfirmation";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Error: Exception during count query: " + ex.Message;
                    }
                }

                using (SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection))
                {
                    commandUpdate.Parameters.AddWithValue("@DriverName", driverName);
                    commandUpdate.Parameters.AddWithValue("@CurrentDate", currentDate);

                    try
                    {
                        int rowsAffected = commandUpdate.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Success: Rows affected - " + rowsAffected;
                        }
                        else
                        {
                            return "Error: Failed to update data";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Error: Exception during update query: " + ex.Message;
                    }
                }
            }
        }

        public Dictionary<string, string> CTStatusChecking(string driverName)
        {
            DateTime currentDate = DateTime.Today;
            string query = "SELECT Endscanning , StartScanning FROM EndScanning WHERE DriveName = '" + driverName + "' AND Date = '" + currentDate.ToString("yyyy-MM-dd") + "';";

            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are any rows
                        if (!reader.HasRows)
                        {
                            resultDictionary.Add("success", "true");
                            resultDictionary.Add("endScanning", "false");
                            resultDictionary.Add("startScanning", "false");
                            resultDictionary.Add("message", "Both values are false.");
                        }
                        else
                        {
                            // Read the values from the database
                            reader.Read();
                            bool endScanning1 = Convert.ToBoolean(reader["EndScanning"]);
                            bool startScanning1 = Convert.ToBoolean(reader["StartScanning"]);

                            resultDictionary.Add("success", "true");
                            resultDictionary.Add("endScanning", endScanning1.ToString());
                            resultDictionary.Add("startScanning", startScanning1.ToString());
                        }
                    }
                }
            }

            return resultDictionary;
        }

    }
}
