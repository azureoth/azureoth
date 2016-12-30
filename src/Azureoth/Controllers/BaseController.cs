using Azureoth.Database;
using Microsoft.AspNetCore.Mvc;

namespace Azureoth.Controllers
{
    public class BaseController : Controller
    {
        protected AzureothDbContext _context;

        public BaseController(AzureothDbContext context)
        {
            _context = context;
        }
    }
}
