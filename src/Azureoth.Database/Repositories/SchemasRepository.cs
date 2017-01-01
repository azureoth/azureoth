
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

        public Schema GetAppSchema(Guid appId)
        {
            var maxValue = GetAll(s => s.ApplicationId == appId).Max(x => x.Version);
            return GetAll(s=> s.ApplicationId == appId).First(a => a.Version == maxValue);
        }

        public int GetNextSchemaVersionNumber(Guid appId)
        {
            var maxValue = GetAll(s => s.ApplicationId == appId).Max(x => x.Version);
            return maxValue + 1;
        }
    }
}
