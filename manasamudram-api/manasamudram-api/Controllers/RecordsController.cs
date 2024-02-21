using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RepositoryADO;
using Models;

namespace manasamudram_api.Controllers
{
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
    }
}
