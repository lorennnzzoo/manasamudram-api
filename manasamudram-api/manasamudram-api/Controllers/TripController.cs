﻿using RepositoryADO;
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


    }
}
