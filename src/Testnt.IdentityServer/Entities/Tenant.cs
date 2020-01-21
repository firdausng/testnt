using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testnt.IdentityServer.Entities
{
    public class Tenant: BaseEntity
    {
        public List<ApplicationUser> Users { get; set; }
        public string Name { get; set; }
    }
}
