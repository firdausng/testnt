using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Testnt.Idp.Domain.Entities;

namespace Testnt.Idp.Infra.Data
{
    public class TenantUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public TenantUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("TenantId", user.TenantId.ToString()));
            return identity;
        }
    }
}
