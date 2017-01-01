using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azureoth.Datastructures
{
    public class JsonTableMeta
    {
        public IEnumerable<string> PrimaryKeyColumns;
        public IEnumerable<string> RequiredColumns;
        public IEnumerable<JsonIndex> Indices;
    }
}
