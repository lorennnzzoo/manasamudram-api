using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;

namespace manasamudram_api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {

        private ManasamudramEntities dbContext = new ManasamudramEntities(); 

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(App_Users newUser)
        {
            try
            {
                if (newUser == null)
                    return BadRequest("Invalid data");

                
                if (dbContext.App_Users.Any(u => u.UserName == newUser.UserName))
                    return Conflict(); 

                
                dbContext.App_Users.Add(newUser);
                dbContext.SaveChanges();

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

                
                var user = dbContext.App_Users.FirstOrDefault(u => u.UserName == loginUser.UserName && u.Password == loginUser.Password);

               
                if (user != null)
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
