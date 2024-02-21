using RepositoryADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace manasamudram_api.Controllers
{
    [RoutePrefix("api/Scan")]
    public class ScanController : ApiController
    {
        ScanOperations sop = new ScanOperations();
        [HttpGet]
        [Route("insertendscanning")]
        public IHttpActionResult InsertEndScanning(string driverName, string tripNo)
        {
            try
            {
                string result = sop.InsertEndScan(driverName, tripNo);
                
                if (result.StartsWith("Success"))
                {
                    return Ok(new { success = true, message = result });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("CTStatusChecking")]
        public IHttpActionResult CTStatusChecking(string driverName)
        {
            try
            {
                var result = sop.CTStatusChecking(driverName);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }



}
