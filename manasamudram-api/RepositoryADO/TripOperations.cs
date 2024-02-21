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
    }
}
