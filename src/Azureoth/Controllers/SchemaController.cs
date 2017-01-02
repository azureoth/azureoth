using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azureoth.Management;
using Azureoth.Modules.SQLdb.Datastructures.Schema;
using Azureoth.Database;
using Microsoft.AspNetCore.Authorization;

namespace Azureoth.Controllers
{
    //[Authorize]
    public class SchemaController : BaseController
    {
        public SchemaController(AzureothDbContext context) : base(context)
        {
            SchemaManager.SetContext(context);
        }

        [Route("apps/{appId}/schema")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSchema(string appId)
        {
            return Ok(SchemaManager.GetSchema(User.Identity.Name, appId));
        }

        [Route("apps/{appId}/schema")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostSchema(string appId, [FromBody] Dictionary<string, JsonTable> schema)
        {
             return Ok(SchemaManager.AddSchema(User.Identity.Name, appId, schema));
        }

        [Route("apps/{appId}/schema")]
        [HttpPut]
        public ActionResult PutSchema(string appId, [FromBody] Dictionary<string, JsonTable> schema)
        {
             return Ok(SchemaManager.UpdateSchema(User.Identity.Name, appId, schema));
        }
    }
}
