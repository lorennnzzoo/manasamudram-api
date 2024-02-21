using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using RepositoryADO;
using System.Web.Http.Cors;

namespace manasamudram_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {

        private ManasamudramEntities dbContext = new ManasamudramEntities();
        UserAccountOperations ua = new UserAccountOperations();
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(App_Users newUser)
        {
            try
            {
                if (newUser == null)
                    return BadRequest("Invalid data");


                
                if (ua.UserExists(newUser))
                    return Conflict();


                
               

                return Ok("Registration successful");
            }
            catch (Exception ex)
            {
                
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(App_Users loginUser)
        {
            try
            {
                if (loginUser == null)
                    return BadRequest("Invalid data");

                
                if (ua.AuthenticateUser(loginUser.UserName, loginUser.Password))
                    return Ok("Login successful");
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
               
                return InternalServerError(ex);
            }
        }
    }
}
