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
    }
}
