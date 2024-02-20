using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RepositoryADO;

namespace manasamudram_api.Controllers
{
    public class HouseHoldController : ApiController
    {

        

        [HttpGet]
        [Route("api/householders")] 
        public IHttpActionResult GetHouseHolders()
        {
            List<Householderviewmodel> resultList = new List<Householderviewmodel>();
            try
            {
                HouseHoldOperations hop = new HouseHoldOperations();
                resultList=hop.GetAllHouseHolders();

                return Ok(resultList);
            }
            catch (Exception ex)
            {
              
                return InternalServerError(ex);
            }
        }
    }
}
