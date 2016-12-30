using Azureoth.Database;
using Azureoth.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azureoth.Management
{
    public static class UsersManager
    {
        private static AzureothDbContext _context;
        public static void SetContext(AzureothDbContext context)
        {
            _context = context;
        }

        public static IEnumerable<User> GetAllUsers()
        {
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                return db.UsersRepository.GetAll();
            }
        }

        public static void AddUser(string name)
        {
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                User newUser = new User();
                newUser.Name = name;
                db.UsersRepository.Create(newUser);
                db.Save();
            }
        }
    }
}
