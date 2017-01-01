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

            SchemaBuilderFactory factory = new SchemaBuilderFactory();
            var databaseConnectionString = "Data Source=(localdb)\\Azureoth;Initial Catalog=Azureoth;Integrated Security=False;MultipleActiveResultSets=True;App=EntityFramework";
            var schemaBuilder = factory.CreateSchemaBuilder(databaseConnectionString, databaseConnectionString, "D:/Logs/");
            schemaBuilder.CreateSchema(schema, appId).GetAwaiter().GetResult();

            Guid toAdd = Guid.NewGuid();
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                Schema newschema = new Schema();
                newschema.OwnerName = userName;
                newschema.ApplicationId = Guid.Parse(appId);
                newschema.Content = JsonConvert.SerializeObject(schema);
                newschema.Version = db.SchemasRepository.GetNextSchemaVersionNumber(Guid.Parse(appId));
                db.SchemasRepository.Create(newschema);
                db.Save();
            }

            return true;
        }
        public static bool UpdateSchema(string userName, string appId, Dictionary<string, JsonTable> schema)
        {
            throw new NotImplementedException();
            ParamValidators.ValidateAppId(appId);
            //getid?
            Guid toAdd = Guid.NewGuid();
            using (var db = new AzureothDbUnitOfWork(_context))
            {
                Schema newschema = new Schema();
                newschema.OwnerName = userName;
                newschema.ApplicationId = Guid.Parse(appId);
                newschema.Content = JsonConvert.SerializeObject(schema);
                newschema.Version = db.SchemasRepository.GetNextSchemaVersionNumber(Guid.Parse(appId));
                db.SchemasRepository.Update(newschema);
                db.Save();
            }

            return true;
        }
    }
}
