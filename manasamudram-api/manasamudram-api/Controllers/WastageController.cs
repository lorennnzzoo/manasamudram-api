﻿using Models;
using RepositoryADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace manasamudram_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Wastage")]
    public class WastageController : ApiController
    {
        WastageOperations wop = new WastageOperations();
        [HttpGet]
        [Route("getwetwastecollected")]
        public IHttpActionResult GetWetWasteCollected()
        {
            try
            {
                TotalWetWastageConfirm TW = wop.GetWetWasteCollected();
                return Ok(TW);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




        [HttpGet]
        [Route("getdrywastecollected")]
        public IHttpActionResult GetDryWasteCollected()
        {
            try
            {
                TotalDryWastageConfirm TW = wop.GetDryWasteCollected();
                return Ok(TW);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("gethhwastecollected")]
        public IHttpActionResult GetHHWasteCollected()
        {
            try
            {
                TotalHHWastageConfirm TW = wop.GetHHWasteCollected();
                return Ok(TW);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("getmixedwastecollected")]
        public IHttpActionResult GetMixedWasteCollected()
        {
            try
            {
                TotalMixedWastageConfirm TW = wop.GetMixedWasteCollected();
                return Ok(TW);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpPost]
        [Route("getwetwastecollected")]
        public IHttpActionResult PostWetWasteCollected(TotalWetWastageConfirm TW)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool effetced = wop.InsertWetWasteCollected(TW);
                    if(effetced)
                    {
                        return Ok("Wet waste data successfully inserted.");
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
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("getdrywastecollected")]
        public IHttpActionResult PostDryWasteCollected(TotalDryWastageConfirm TW)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool effetced = wop.InsertDryWasteCollected(TW);
                    if (effetced)
                    {
                        return Ok("Wet waste data successfully inserted.");
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
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("gethhwastecollected")]
        public IHttpActionResult PostHHWasteCollected(TotalHHWastageConfirm TW)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool effetced = wop.InsertHHWasteCollected(TW);
                    if (effetced)
                    {
                        return Ok("Wet waste data successfully inserted.");
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
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("getmixedwastecollected")]
        public IHttpActionResult PostMixedWasteCollected(TotalMixedWastageConfirm TW)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool effetced = wop.InsertMixedWasteCollected(TW);
                    if (effetced)
                    {
                        return Ok("Wet waste data successfully inserted.");
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
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [Route("WastageConfirmation/{id}")]
        public IHttpActionResult WastageConfirmation(Guid id)
        {
            try
            {
                HouseHold hh = wop.WastageConfirmation(id);
                if(hh != null)
                {
                    return Ok(hh);
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


        [HttpPost]
        [Route("InsertConfirmation")]
        public IHttpActionResult InsertConfirmation(WasteConfirm WC)
        {
            try
            {
                int re = wop.WastageConfirmation(WC);
                if(re>0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, show an error message, etc.)
                return InternalServerError(ex);
            }
        }
    }
}
