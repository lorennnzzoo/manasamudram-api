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
    public class WastageOperations
    {
        private ManasamudramEntities context = new ManasamudramEntities();
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();



        public TotalWetWastageConfirm GetWetWasteCollected()
        {
            DateTime dtt = Convert.ToDateTime("1970-01-01 00:00:00");
            string dt = "";

            string query = "SELECT TOP 1 DateTime FROM TotalWetWastage  ORDER BY WetWastageId DESC";

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            var result1 = command.ExecuteScalar();
            connection.Close();
            if (result1 != DBNull.Value && result1 != null)
            {
                dtt = Convert.ToDateTime(result1);
                dt = dtt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string sqlQuery = "SELECT WetWasteCollected, DryWasteCollected, HHWasteCollected, MixedWasteCollected FROM WastageInfo WHERE   DateTimeWasteLogged >'" + dt + "'";

            decimal totalWetWaste = 0;

            int uniqueHouseIdsCollectedCount = 0;
            using (SqlConnection connection2 = new SqlConnection(connectionstring))
            using (SqlCommand command2 = new SqlCommand(sqlQuery, connection2))
            {
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command2))
                {
                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        totalWetWaste += Convert.ToDecimal(row["WetWasteCollected"]);
                    }
                }
            }

            DateTime toDatetime = DateTime.Now;
            string query2 = "SELECT COUNT(DISTINCT HouseId) AS UniqueHouseIdsCollectedCount " +
                           "FROM WastageConfirmation " +
                           "WHERE datetime BETWEEN '" + dt + "' AND '" + toDatetime + "' " +
                           "AND ServiceGiven = 1";

            SqlConnection connection3 = new SqlConnection(connectionstring);
            connection3.Open();
            using (SqlCommand command2 = new SqlCommand(query2, connection3))
            {
                try
                {
                    var result2 = command2.ExecuteScalar();

                    if (result2 != DBNull.Value && result2 != null)
                    {
                        uniqueHouseIdsCollectedCount = Convert.ToInt32(result2);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            connection3.Close();

            TotalWetWastageConfirm TW = new TotalWetWastageConfirm
            {
                WetWasteCollected = totalWetWaste,
                HousesCollected = uniqueHouseIdsCollectedCount
            };

            return TW;
        }

        public TotalDryWastageConfirm GetDryWasteCollected()
        {
            DateTime dtt = Convert.ToDateTime("1970-01-01 00:00:00");
            string dt = "";

            string query = "SELECT TOP 1 DateTime FROM TotalDryWastage ORDER BY DryWastageId DESC";

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            var result1 = command.ExecuteScalar();
            connection.Close();
            if (result1 != DBNull.Value && result1 != null)
            {
                dtt = Convert.ToDateTime(result1);
                dt = dtt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string sqlQuery = "SELECT WetWasteCollected, DryWasteCollected, HHWasteCollected, MixedWasteCollected FROM WastageInfo WHERE DateTimeWasteLogged >'" + dt + "'";

            decimal totalDryWaste = 0;

            int uniqueHouseIdsCollectedCount = 0;
            using (SqlConnection connection2 = new SqlConnection(connectionstring))
            using (SqlCommand command2 = new SqlCommand(sqlQuery, connection2))
            {
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command2))
                {
                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        totalDryWaste += Convert.ToDecimal(row["DryWasteCollected"]);
                    }
                }
            }

            DateTime toDatetime = DateTime.Now;
            string query2 = "SELECT COUNT(DISTINCT HouseId) AS UniqueHouseIdsCollectedCount " +
                           "FROM WastageConfirmation " +
                           "WHERE datetime BETWEEN '" + dt + "' AND '" + toDatetime + "' " +
                           "AND ServiceGiven = 1";

            SqlConnection connection3 = new SqlConnection(connectionstring);
            connection3.Open();
            using (SqlCommand command2 = new SqlCommand(query2, connection3))
            {
                try
                {
                    var result2 = command2.ExecuteScalar();

                    if (result2 != DBNull.Value && result2 != null)
                    {
                        uniqueHouseIdsCollectedCount = Convert.ToInt32(result2);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            connection3.Close();

            TotalDryWastageConfirm TW = new TotalDryWastageConfirm
            {
                DryWasteCollected = totalDryWaste,
                HousesCollected = uniqueHouseIdsCollectedCount
            };

            return TW;
        }

        public TotalHHWastageConfirm GetHHWasteCollected()
        {
            DateTime dtt = Convert.ToDateTime("1970-01-01 00:00:00");
            string dt = "";

            string query = "SELECT TOP 1 DateTime FROM TotalHHWastage ORDER BY HHWastageId DESC";

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            var result1 = command.ExecuteScalar();
            connection.Close();
            if (result1 != DBNull.Value && result1 != null)
            {
                dtt = Convert.ToDateTime(result1);
                dt = dtt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string sqlQuery = "SELECT WetWasteCollected, DryWasteCollected, HHWasteCollected, MixedWasteCollected FROM WastageInfo WHERE DateTimeWasteLogged >'" + dt + "'";

            decimal totalHHWaste = 0;

            int uniqueHouseIdsCollectedCount = 0;
            using (SqlConnection connection2 = new SqlConnection(connectionstring))
            using (SqlCommand command2 = new SqlCommand(sqlQuery, connection2))
            {
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command2))
                {
                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        totalHHWaste += Convert.ToDecimal(row["HHWasteCollected"]);
                    }
                }
            }

            DateTime toDatetime = DateTime.Now;
            string query2 = "SELECT COUNT(DISTINCT HouseId) AS UniqueHouseIdsCollectedCount " +
                           "FROM WastageConfirmation " +
                           "WHERE datetime BETWEEN '" + dt + "' AND '" + toDatetime + "' " +
                           "AND ServiceGiven = 1";

            SqlConnection connection3 = new SqlConnection(connectionstring);
            connection3.Open();
            using (SqlCommand command2 = new SqlCommand(query2, connection3))
            {
                try
                {
                    var result2 = command2.ExecuteScalar();

                    if (result2 != DBNull.Value && result2 != null)
                    {
                        uniqueHouseIdsCollectedCount = Convert.ToInt32(result2);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            connection3.Close();

            TotalHHWastageConfirm TW = new TotalHHWastageConfirm
            {
                HHWasteCollected = totalHHWaste,
                HousesCollected = uniqueHouseIdsCollectedCount
            };

            return TW;
        }

        public TotalMixedWastageConfirm GetMixedWasteCollected()
        {
            DateTime dtt = Convert.ToDateTime("1970-01-01 00:00:00");
            string dt = "";

            string query = "SELECT TOP 1 DateTime FROM TotalMixedWastage ORDER BY MixedWastageId DESC";

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            var result1 = command.ExecuteScalar();
            connection.Close();
            if (result1 != DBNull.Value && result1 != null)
            {
                dtt = Convert.ToDateTime(result1);
                dt = dtt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string sqlQuery = "SELECT WetWasteCollected, DryWasteCollected, HHWasteCollected, MixedWasteCollected FROM WastageInfo WHERE DateTimeWasteLogged >'" + dt + "'";

            decimal totalMixedWaste = 0;

            int uniqueHouseIdsCollectedCount = 0;
            using (SqlConnection connection2 = new SqlConnection(connectionstring))
            using (SqlCommand command2 = new SqlCommand(sqlQuery, connection2))
            {
                DataTable dataTable = new DataTable();

                using (SqlDataAdapter adapter = new SqlDataAdapter(command2))
                {
                    adapter.Fill(dataTable);
                }

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        totalMixedWaste += Convert.ToDecimal(row["MixedWasteCollected"]);
                    }
                }
            }

            DateTime toDatetime = DateTime.Now;
            string query2 = "SELECT COUNT(DISTINCT HouseId) AS UniqueHouseIdsCollectedCount " +
                           "FROM WastageConfirmation " +
                           "WHERE datetime BETWEEN '" + dt + "' AND '" + toDatetime + "' " +
                           "AND ServiceGiven = 1";

            SqlConnection connection3 = new SqlConnection(connectionstring);
            connection3.Open();
            using (SqlCommand command2 = new SqlCommand(query2, connection3))
            {
                try
                {
                    var result2 = command2.ExecuteScalar();

                    if (result2 != DBNull.Value && result2 != null)
                    {
                        uniqueHouseIdsCollectedCount = Convert.ToInt32(result2);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception, log it, or return an appropriate response
                }
            }
            connection3.Close();

            TotalMixedWastageConfirm TW = new TotalMixedWastageConfirm
            {
                MixedWasteCollected = totalMixedWaste,
                HousesCollected = uniqueHouseIdsCollectedCount
            };

            return TW;
        }


        public bool InsertWetWasteCollected(TotalWetWastageConfirm TW)
        {
            TotalWetWastage TWW = new TotalWetWastage
            {
                DateTime = DateTime.Now,
                WetWasteCollected = TW.WetWasteCollected,
                WetWasteReceived = TW.WetWasteReceived,
                WetWasteProcessed = TW.WetWasteProcessed,
                WetWasteRecovery = TW.WetWasteRecovery,
                HousesCollected = TW.HousesCollected
            };


            context.TotalWetWastages.Add(TWW);
            int rowsAffected = context.SaveChanges();
            
            if (rowsAffected > 0)
            {                
                return true;
            }
            else
            {                
                return false;
            }
        }


        public bool InsertDryWasteCollected(TotalDryWastageConfirm TW)
        {
            TotalDryWastage TWW = new TotalDryWastage
            {
                DateTime = DateTime.Now,

                DryWasteCollected = TW.DryWasteCollected,

                DryWasteReceived = TW.DryWasteReceived,

                DryWasteProcessed = TW.DryWasteProcessed,

                DryWasteRecovery = TW.DryWasteRecovery,
                HousesCollected = TW.HousesCollected
            };
            context.TotalDryWastages.Add(TWW);            
            int rowsAffected = context.SaveChanges();

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertHHWasteCollected(TotalHHWastageConfirm TW)
        {
            TotalHHwastage TWW = new TotalHHwastage
            {
                DateTime = DateTime.Now,

                HHWasteCollected = TW.HHWasteCollected,
                TotalHHHSafelyDisposed = TW.TotalHHHSafelyDisposed,
                HousesCollected = TW.HousesCollected
            };
            context.TotalHHwastages.Add(TWW);            
            int rowsAffected = context.SaveChanges();

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertMixedWasteCollected(TotalMixedWastageConfirm TW)
        {
            TotalMixedWastage TWW = new TotalMixedWastage
            {
                DateTime = DateTime.Now,

                MixedWasteCollected = TW.MixedWasteCollected,
                TotalMixedWasteDisposed = TW.TotalMixedWasteDisposed,
                MixedWasteRecovery = TW.MixedWasteRecovery,
                HousesCollected = TW.HousesCollected
            };
            context.TotalMixedWastages.Add(TWW);            
            int rowsAffected = context.SaveChanges();

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public HouseHold WastageConfirmation(Guid id)
        {
            SqlConnection con = new SqlConnection(connectionstring);
            con.Open();
            HouseHold hh = null;
            SqlCommand cmd = new SqlCommand("SELECT * FROM HouseHold WHERE QRUniqueID='" + id + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                hh = new HouseHold
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = Convert.ToString(reader["Name"]),
                    PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                    State = Convert.ToString(reader["State"]),
                    District = Convert.ToString(reader["District"]),
                    DOORNo = Convert.ToString(reader["DOORNo"]),
                    NumberOfPersons = Convert.ToString(reader["NumberOfPersons"])
                };
            }
            con.Close();

            if (hh != null)
            {
                return hh;
            }
            else
            {
                return null;
            }
        }

        public int WastageConfirmation(WasteConfirm WC)
        {
            WastageConfirmation WCC = new WastageConfirmation
            {
                Datetime = WC.Datetime,
                HouseId = WC.HouseId,
                ServiceGiven = WC.ServiceGiven,
                DriverName = WC.DriverName,
                IsScannestatus = true,
                Trip = WC.Trip
            };

            context.WastageConfirmations.Add(WCC);
            int rowsefected=context.SaveChanges();

            return rowsefected;
        }
    }
}
