using Microsoft.AspNetCore.Mvc;
using Azureoth.Database;
using Azureoth.Management;
using Microsoft.AspNetCore.Authorization;
using Azureoth.Database.Models;
using Azureoth.Datastructures;
using System.Linq;

namespace Azureoth.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(AzureothDbContext context) : base(context)
        {
            ApplicationManager.SetContext(context);
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var apps = ApplicationManager.GetUserApps(User.Identity.Name).OrderBy(a => a.Title);
                return View(apps);
            }

            return View();
        }

        [Authorize]
        [HttpGet("home/app/{id}")]
        public IActionResult App(string id)
        {
            var app = ApplicationManager.GetUserApp(User.Identity.Name, id);
            return View(app);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
