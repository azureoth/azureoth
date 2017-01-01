using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Azureoth.Database.Models
{
    public class Application
    {
        public Guid Id { get; set; }

        public string OwnerName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }


    }
}
