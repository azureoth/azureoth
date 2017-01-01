
using System;
using System.Collections.Generic;
using Azureoth.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Azureoth.Database.Repositories
{
    public class ApplicationsRepository : BaseRepository<Application>
    {
        public ApplicationsRepository(AzureothDbContext context) : base(context)
        {
        }

        public IEnumerable<Application> GetUserApps(string userName)
        {
            return GetAll(a => a.OwnerName == userName);
        }
    }
}
