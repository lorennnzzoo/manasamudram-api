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


      
    }
}
