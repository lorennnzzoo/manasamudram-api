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
    public class HouseHoldOperations
    {
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
            
    }
}
