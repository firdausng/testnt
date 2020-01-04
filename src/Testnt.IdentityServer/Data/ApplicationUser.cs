using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testnt.IdentityServer.Data.Entity;

namespace Testnt.IdentityServer.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsEnabled { get; set; }
        public Tenant Tenant { get; set; }
    }

}
