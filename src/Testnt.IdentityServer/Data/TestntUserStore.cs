using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testnt.IdentityServer.Entities;

namespace Testnt.IdentityServer.Data
{
    public class TestntUserStore : UserStore<ApplicationUser, ApplicationRole, TestntIdentityDbContext, Guid>
    {
        public Guid TenantId { get; set; }
        public TestntUserStore(TestntIdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (user.TenantId == null)
            {
                throw new ArgumentNullException(nameof(user.TenantId));
            }


            return await base.CreateAsync(user, cancellationToken);
        }
    }
}
