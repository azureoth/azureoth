using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azureoth.Datastructures;
using Azureoth.Utility;
using Azureoth.Database;

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
               
            return new Dictionary<string, JsonTable>();
        }
        public static bool AddSchema(string userName, string appId, Dictionary<string, JsonTable> schema)
        {
            ParamValidators.ValidateAppId(appId);

            return true;
        }
        public static bool UpdateSchema(string userName, string appId, Dictionary<string, JsonTable> schema)
        {
            ParamValidators.ValidateAppId(appId);

            return true;
        }
    }
}
