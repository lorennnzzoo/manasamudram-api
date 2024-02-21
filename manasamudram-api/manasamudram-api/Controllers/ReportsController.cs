using RepositoryADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace manasamudram_api.Controllers
{
    [RoutePrefix("api/Reports")]
    public class ReportsController : ApiController
    {
        ReportsOperations Top = new ReportsOperations();
      
    }
}
