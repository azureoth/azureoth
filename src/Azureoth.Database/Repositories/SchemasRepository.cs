
using System;
using System.Collections.Generic;
using Azureoth.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Azureoth.Database.Repositories
{
    public class SchemasRepository : BaseRepository<Schema>
    {
        public SchemasRepository(AzureothDbContext context) : base(context)
        {
        }
    }
}
