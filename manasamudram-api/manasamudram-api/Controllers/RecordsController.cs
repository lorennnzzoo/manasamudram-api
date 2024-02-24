using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RepositoryADO;
using Models;
using System.Web.Http.Cors;

namespace manasamudram_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Records")]
    public class RecordsController : ApiController
    {        
        RecordOperations rop = new RecordOperations();

        [HttpGet]
        [Route("PreviousRecords")]
        public IHttpActionResult GetPreviousRecords()
        {
            try
            {
                List<MicRecordingModel> mic = rop.GetPreviousRecords();
                return Ok(mic);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }




        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Insertaudio")]
        public IHttpActionResult InsertAudioData([FromBody] AudioDataModel audioDataModel)
        {
            if (audioDataModel == null)
            {
                return BadRequest("Audio data model is null.");
            }

            byte[] audioBytes = audioDataModel.Audio;
            string[] timeComponents = audioDataModel.Duration.Split(':');
            int minutes = int.Parse(timeComponents[0]);
            int seconds = int.Parse(timeComponents[1]);
            int totalduration = (minutes * 60) + seconds;

            using (ManasamudramEntities dbEntities = new ManasamudramEntities())
            {
                var vehicles = dbEntities.VehiclesForMics.ToList();

                if (vehicles.Count == 0)
                {
                    return BadRequest("No Vehicles Found To Add The Records");
                }

                foreach (var vehicle in vehicles)
                {
                    MicRecording recording = new MicRecording
                    {
                        VehicleId = vehicle.Id,
                        DateTimeRecorded = DateTime.Now,
                        IsRead = false,
                        RecordedData = audioBytes,
                        LengthOfAudio = totalduration
                    };

                    dbEntities.MicRecordings.Add(recording);
                }

                dbEntities.SaveChanges();
            }
            return Ok("Audio data received successfully.");
        }
    }






}
