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
    [RoutePrefix("api/household")]
    public class HouseHoldController : ApiController
    {

        HouseHoldOperations hop = new HouseHoldOperations();

        [HttpGet]
        [Route("gethouseholders")] 
        public IHttpActionResult GetHouseHolders()
        {
            List<Householderviewmodel> resultList = new List<Householderviewmodel>();
            try
            {
                
                resultList=hop.GetAllHouseHolders();

                return Ok(resultList);
            }
            catch (Exception ex)
            {
              
                return InternalServerError(ex);
            }
        }



        [HttpPost]
        [Route("generateqr")]
        public IHttpActionResult GenerateQR(HouseHold model)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    if(hop.GenerateQRs(model))
                    {
                        return Ok("QR code and household data generated successfully");
                    }
                    else
                    {
                        return BadRequest();
                    }
                    
                }

                
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                
                return InternalServerError(ex);
            }
        }


        
    }
}
