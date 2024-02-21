using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Configuration;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Data;

namespace RepositoryADO
{
    public class HouseHoldOperations
    {
        private ManasamudramEntities context = new ManasamudramEntities();
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();
        public  List<Householderviewmodel> GetAllHouseHolders()
        {
            string sqlCommand = "select * from HouseHold";

            List<Householderviewmodel> resultList = new List<Householderviewmodel>();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Householderviewmodel model = new Householderviewmodel();

                            model.SerialNumber = Convert.ToInt32(reader["ID"]);
                            model.QR = Convert.ToString(reader["QRUniqueID"]);
                            model.Name = Convert.ToString(reader["Name"]);
                            model.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                            model.State = Convert.ToString(reader["State"]);
                            model.District = Convert.ToString(reader["District"]);
                            model.DOORNo = Convert.ToString(reader["DOORNo"]);
                            model.NumberOfPersons = Convert.ToInt32(reader["NumberOfPersons"]);

                            resultList.Add(model);
                        }
                    }
                }
            }
            return resultList;
        }

        public bool GenerateQRs(HouseHold model)
        {
            try
            {
                Guid id = Guid.NewGuid();
                string qrUrl = id.ToString();


                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrUrl, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(10);


                byte[] qrCodeBytes;
                using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    qrCodeBytes = stream.ToArray();
                }


                QR qrModel = new QR
                {
                    UniqueID = id,
                    QRData = qrCodeBytes
                };

                context.QRs.Add(qrModel);
                context.SaveChanges();


                HouseHold householdModel = new HouseHold
                {
                    QRUniqueID = qrModel.UniqueID,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    State = "Andhra Pradesh",
                    District = "Chittoor",
                    NumberOfPersons = model.NumberOfPersons,
                    DOORNo=model.DOORNo
                };

                context.HouseHolds.Add(householdModel);
                context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


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

    }
}
