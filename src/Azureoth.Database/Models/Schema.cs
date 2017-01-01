using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azureoth.Database.Models
{
    public class Schema
    {
        public int Id { get; set; }

        public string OwnerName { get; set; }

        public Guid ApplicationId { get; set; } 

        public int Version { get; set; }

        public string Content { get; set; }


    }
}
