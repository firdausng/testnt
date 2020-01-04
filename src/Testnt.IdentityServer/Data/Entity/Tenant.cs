using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testnt.IdentityServer.Data.Entity
{
    public class Tenant: BaseEntity
    {
        public string Name { get; set; }
    }
}
