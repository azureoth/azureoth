using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azureoth.Database;
using Azureoth.Management;

namespace Azureoth.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(AzureothDbContext context) : base(context)
        {
            UsersManager.SetContext(context);
        }

        [Route("users")]
        [HttpPost]
        public ActionResult AddUser([FromBody] string name)
        {
            UsersManager.AddUser(name);
            return Ok();
        }

        [Route("users")]
        [HttpGet]
        public ActionResult GetUsers()
        {
            return Ok(UsersManager.GetAllUsers());
        }
    }
}
