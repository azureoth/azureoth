using Azureoth.Datastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azureoth.Utility;
using Azureoth.Database;
using Azureoth.Database.Models;

namespace Azureoth.Management
{
    public static class ApplicationManager
    {
        private static AzureothDbContext _context;
        public static void SetContext(AzureothDbContext context)
        {
            _context = context;
        }
        public static List<UserApplication> GetUserApps(string userName)
        {
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                db.ApplicationsRepository.GetUserApps(userName);
                db.Save();
            }
            return new List<UserApplication>();
        }

        public static UserApplication GetUserApp(string userName, string appId)
        {
            ParamValidators.ValidateAppId(appId);

            return new UserApplication();
        }

        public static bool AddUserApp(string userName, UserApplication application)
        {
            ParamValidators.ValidateApp(application);

            using (var db = new AzureothDbUnitOfWork(_context))
            {
                Application newApp = new Application();
                newApp.Id = Guid.NewGuid();
                newApp.OwnerName = userName;
                db.ApplicationsRepository.Create(newApp);
                db.Save();
            }

            return true;
        }
        public static bool EditUserApp(string userName, UserApplication application)
        {
            ParamValidators.ValidateApp(application);

            return true;
        }
    }
}
