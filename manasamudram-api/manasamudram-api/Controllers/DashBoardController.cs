using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using RepositoryADO;

namespace manasamudram_api.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashBoardController : ApiController
    {
        DashBoardOperations DBop = new DashBoardOperations();

        [HttpGet]
        [Route("getdashboard")]
        public IHttpActionResult GetDashBoardData(DashBoardViewModel DBVM)
        {
            try
            {
                DashBoard db = new DashBoard();

                db= DBop.GetDashBoardData(DBVM);
                return Ok(db);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }
    }
}
