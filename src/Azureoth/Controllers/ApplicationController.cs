using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azureoth.Datastructures;
using Azureoth.Management;
using Azureoth.Database;
using Microsoft.AspNetCore.Authorization;

namespace Azureoth.Controllers
{
    //[Authorize]
    public class ApplicationController : BaseController
    {
        public ApplicationController(AzureothDbContext context) : base(context)
        {
            ApplicationManager.SetContext(context);
        }

        [Route("apps")]
        [HttpGet]
        public ActionResult GetApps()
        {
            return Ok(ApplicationManager.GetUserApps(User.Identity.Name));
        }

        [Route("apps/{appId}")]
        [HttpGet]
        public ActionResult GetApp(string appId)
        {
            return Ok(ApplicationManager.GetUserApp(User.Identity.Name, appId));
        }

        [Route("apps")]
        [HttpPost]
        public ActionResult PostApp([FromBody] UserApplication application)
        {
            return Ok(ApplicationManager.AddUserApp(User.Identity.Name, application));

        }

        [Route("apps")]
        [HttpPut]
        public ActionResult PutApp([FromBody] UserApplication application)
        {
            return Ok(ApplicationManager.EditUserApp(User.Identity.Name, application));
        }
    }
}
