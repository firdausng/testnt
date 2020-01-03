using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testnt.IdentityServer.Data
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name)
        {
            Name = name;
        }

        public string Description { get; set; }
    }
}
