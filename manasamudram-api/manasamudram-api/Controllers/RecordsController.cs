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
    }
}
