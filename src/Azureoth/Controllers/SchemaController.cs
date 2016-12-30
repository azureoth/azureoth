using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azureoth.Management;
using Azureoth.Datastructures;
using Azureoth.Database;

namespace Azureoth.Controllers
{
    public class SchemaController : BaseController
    {
        public SchemaController(AzureothDbContext context) : base(context)
        {
        }

        [Route("apps/{appId}/schema")]
        [HttpGet]
        public ActionResult GetSchema(string appId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(SchemaManager.GetSchema(User.Identity.Name, appId));
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("apps/{appId}/schema")]
        [HttpPost]
        public ActionResult PostSchema(string appId, [FromBody] Dictionary<string, JsonTable> schema)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(SchemaManager.AddSchema(User.Identity.Name, appId, schema));
            }
            else
            {
                return Unauthorized();
            }
        }

        [Route("apps/{appId}/schema")]
        [HttpPut]
        public ActionResult PutSchema(string appId, [FromBody] Dictionary<string, JsonTable> schema)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(SchemaManager.UpdateSchema(User.Identity.Name, appId, schema));
            }
            else
            {
                return Unauthorized();
            }
        }




    }
}
