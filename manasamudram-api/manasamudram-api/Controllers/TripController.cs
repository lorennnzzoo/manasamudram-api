using Models;
using RepositoryADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace manasamudram_api.Controllers
{
    [RoutePrefix("api/Trip")]
    public class TripController : ApiController
    {
        TripOperations Top = new TripOperations();

        [HttpGet]
        [Route("GetTripCount")]
        public IHttpActionResult GetTrip(string userName)
        {
            try
            {
                string trips = Top.GetTripCount(userName);
                if(trips != null)
                {
                    return Ok(trips);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {       
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("GetTripCount")]
        public IHttpActionResult GetTripCount()
        {
            try
            {
                int tripcount = Top.GetTripCount();
                return Ok(tripcount);
            }
            catch (Exception ex)
            {                
                return InternalServerError(ex);
            }
        }



        [HttpPost]
        [Route("UpdateTrip")]
        public IHttpActionResult UpdateTrip(string TripCount, string VehicleNumber, int NoofWorkers, string driverName)
        {
            try
            {
                Top.UpdateTripDetails(TripCount, VehicleNumber, NoofWorkers, driverName);
                return Ok(new { success = true, message = "SUCCESS" });
            }
            catch (Exception ex)
            {                
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("TripData")]
        public IHttpActionResult TripData(tripdata WI)
        {
            DateTime currentDate = DateTime.Today;

            if (ModelState.IsValid)
            {
                try
                {
                    WastageInfo W = new WastageInfo
                    {
                        DateTimeWasteLogged = DateTime.Now,
                        WetWasteCollected = WI.WetWasteCollected,
                        DryWasteCollected = WI.DryWasteCollected,
                        HHWasteCollected = WI.HHWasteCollected,
                        MixedWasteCollected = WI.MixedWasteCollected,
                        DriverName = WI.DriverName,
                    };

                    hh.WastageInfoes.Add(W);
                    hh.SaveChanges();

                    string query2 = "DELETE FROM Endscanning WHERE StartScanning = '1' AND Endscanning = '1' AND DriveName = @DriverName AND Date = @CurrentDate";

                    using (SqlConnection connection = new SqlConnection(connectionstring))
                    {
                        using (SqlCommand command = new SqlCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@DriverName", WI.DriverName);
                            command.Parameters.AddWithValue("@CurrentDate", currentDate.ToString("yyyy-MM-dd"));

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    
                    return Ok(new { success = true, message = "Data saved successfully." });
                }
                catch (Exception ex)
                {
                    
                    return InternalServerError(ex);
                }
            }

            
            return BadRequest(ModelState);
        }
    }
}
