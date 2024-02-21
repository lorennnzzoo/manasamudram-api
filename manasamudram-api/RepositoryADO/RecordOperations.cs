using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;

namespace RepositoryADO
{
    public class RecordOperations
    {
        private ManasamudramEntities context = new ManasamudramEntities();
        public string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ManasamudramString"].ToString();

        public List<MicRecordingModel> GetPreviousRecords()
        {
            string query = "SELECT * FROM MicRecording";

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    List<MicRecordingModel> micRecordings = new List<MicRecordingModel>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MicRecordingModel micRecording = new MicRecordingModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                DateTimeRecorded = reader.GetDateTime(reader.GetOrdinal("DateTimeRecorded")),
                                IsRead = reader.GetBoolean(reader.GetOrdinal("IsRead")),
                                RecordedData = (byte[])reader["RecordedData"],
                                LengthOfAudio = reader.GetInt32(reader.GetOrdinal("LengthOfAudio")),
                            };

                            micRecordings.Add(micRecording);
                        }
                    }

                    return micRecordings;
                }
            }
        }
    }
}
