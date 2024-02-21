using RepositoryADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using System.Web.Http.Cors;

namespace manasamudram_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Reports")]
    public class ReportsController : ApiController
    {
        ReportsOperations Rop = new ReportsOperations();
        [HttpPost]
        [Route("GetReports")]
        public IHttpActionResult PostReports(GetReportViewModel GRVM)
        {            
            try
            {
                ReportsApiResponse rpi = Rop.GetReports(GRVM);
                return Ok(rpi);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
