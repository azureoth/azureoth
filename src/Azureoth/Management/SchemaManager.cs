using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azureoth.Utility;
using Azureoth.Database;
using Newtonsoft.Json;
using Azureoth.Modules.SQLdb.Datastructures.Schema;
using Azureoth.Database.Models;
using Azureoth.Modules.SQLdb;

namespace Azureoth.Management
{
    public static class SchemaManager
    {
        private static AzureothDbContext _context;
        public static void SetContext(AzureothDbContext context)
        {
            _context = context;
        }
        public static Dictionary<string, JsonTable> GetSchema(string userName, string appId)
        {
            ParamValidators.ValidateAppId(appId);

            using (var db = new AzureothDbUnitOfWork(_context))
            {
                var schema = db.SchemasRepository.GetAppSchema(Guid.Parse(appId));

                Dictionary<string, JsonTable> ret = JsonConvert.DeserializeObject<Dictionary<string, JsonTable>>(schema.Content);

                return ret;

            }
        }
        public static bool AddSchema(string userName, string appId, Dictionary<string, JsonTable> schema)
        {
            ParamValidators.ValidateAppId(appId);
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                if (db.SchemasRepository.GetAll(p => p.ApplicationId == Guid.Parse(appId)).Any())
                    return false;
                var appName = db.ApplicationsRepository.GetAll(a => a.Id == Guid.Parse(appId)).First().Title;

                Guid toAdd = Guid.NewGuid();

                Schema newschema = new Schema();
                newschema.OwnerName = userName;
                newschema.ApplicationId = Guid.Parse(appId);
                newschema.Content = JsonConvert.SerializeObject(schema);
                newschema.Version = 1;
                db.SchemasRepository.Create(newschema);

                ISchemaBuilderFactory factory = new SchemaBuilderFactory();
                var primaryConnectionString = "Server=(localdb)\\Azureoth;Database=AzureothProd;";
                var secondaryConnectionString = "Server=(localdb)\\Azureoth;Database=AzureothStaging;";
                var schemaBuilder = factory.CreateSchemaBuilder(primaryConnectionString, secondaryConnectionString, "D:/Logs/");
                schemaBuilder.CreateSchema(schema, appName).GetAwaiter().GetResult();

                db.Save();
            }

            return true;
        }
        public static bool UpdateSchema(string userName, string appId, Dictionary<string, JsonTable> schema)
        {
             ParamValidators.ValidateAppId(appId);
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                if (db.SchemasRepository.GetAll(p => p.ApplicationId == Guid.Parse(appId)).Any())
                    return false;
                var app = db.ApplicationsRepository.GetAll(a => a.Id == Guid.Parse(appId)).First();

                Guid toAdd = Guid.NewGuid();

                Schema oldSchema = db.SchemasRepository.GetAppSchema(Guid.Parse(appId));

                Schema newschema = new Schema();
                newschema.OwnerName = userName;
                newschema.ApplicationId = Guid.Parse(appId);
                newschema.Content = JsonConvert.SerializeObject(schema);
                newschema.Version = db.SchemasRepository.GetNextSchemaVersionNumber(Guid.Parse(appId));
                db.SchemasRepository.Create(newschema);


                ISchemaBuilderFactory factory = new SchemaBuilderFactory();
                var primaryConnectionString = "Server=(localdb)\\Azureoth;Database=AzureothProd;";
                var secondaryConnectionString = "Server=(localdb)\\Azureoth;Database=AzureothStaging;";
                var schemaBuilder = factory.CreateSchemaBuilder(primaryConnectionString, secondaryConnectionString, "D:/Logs/");
                schemaBuilder.UpdateSchema(JsonConvert.DeserializeObject<Dictionary<string, JsonTable>> (oldSchema.Content),
                    schema, app.Title).GetAwaiter().GetResult();

                db.Save();
            }

            return true;
        }
    }
}
