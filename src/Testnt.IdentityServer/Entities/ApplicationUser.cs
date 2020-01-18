using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Testnt.IdentityServer.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsEnabled { get; set; }
        public Guid TenantId { get; set; }
        public DateTimeOffset LastLogin { get; set; }
    }

}
