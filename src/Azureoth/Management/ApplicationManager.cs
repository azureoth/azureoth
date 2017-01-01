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
                var apps = db.ApplicationsRepository.GetUserApps(userName);

                List<UserApplication> ret = new List<UserApplication>();

                foreach (var app in apps)
                {
                    ret.Add(new UserApplication() {
                        Title = app.Title,
                        Description = app.Description,
                        Id = app.Id.ToString()
                    });
                }

                return ret;
            }
        }

        public static UserApplication GetUserApp(string userName, string appId)
        {
            ParamValidators.ValidateAppId(appId);
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                var apps = db.ApplicationsRepository.GetAll(a=> a.Id == Guid.Parse(appId));

                List<UserApplication> ret = new List<UserApplication>();

                foreach (var app in apps)
                {
                    ret.Add(new UserApplication()
                    {
                        Title = app.Title,
                        Description = app.Description,
                        Id = app.Id.ToString()
                    });
                }

                return ret.First();
            }
        }

        public static Guid AddUserApp(string userName, UserApplication application)
        {
            ParamValidators.ValidateApp(application);
            Guid toAdd = Guid.NewGuid();
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                Application newApp = new Application();
                newApp.Id = toAdd;
                newApp.OwnerName = userName;
                newApp.Title = application.Title;
                newApp.Description = application.Description;
                db.ApplicationsRepository.Create(newApp);
                db.Save();
            }

            return toAdd;
        }
        public static bool EditUserApp(string userName, UserApplication application)
        {
            throw new NotImplementedException();
            ParamValidators.ValidateApp(application);

            return true;
        }
    }
}
