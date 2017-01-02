using Microsoft.AspNetCore.Mvc;
using Azureoth.Datastructures;
using Azureoth.Management;
using Azureoth.Database;
namespace Azureoth.Controllers
{
    [Route("apps")]
    public class ApplicationsController : BaseController
    {
        public ApplicationsController(AzureothDbContext context) : base(context)
        {
            ApplicationManager.SetContext(context);
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(ApplicationManager.GetUserApps(User.Identity.Name));
        }

        [HttpGet("{appId}")]
        public IActionResult Get(string appId)
        {
            return Ok(ApplicationManager.GetUserApp(User.Identity.Name, appId));
        }

        [HttpPost]
        public ActionResult Post([FromBody] UserApplication application)
        {
            return Ok(ApplicationManager.AddUserApp(User.Identity.Name, application));
        }

        [HttpPut]
        public ActionResult Put([FromBody] UserApplication application)
        {
            return Ok(ApplicationManager.EditUserApp(User.Identity.Name, application));
        }
    }
}
