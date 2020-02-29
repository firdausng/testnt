using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Testnt.IdentityServer.Common.Attribute;
using Testnt.IdentityServer.Data;
using Testnt.IdentityServer.Entities;

namespace Testnt.IdentityServer.Areas.Admin.Pages.Account
{
    [SecurityHeaders]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TestntIdentityDbContext dbContext;

        public IndexModel(TestntIdentityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Entities.Tenant Tenant { get; set; }
        public List<ApplicationUser> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            var tenantId = Guid.Parse(userClaims.FirstOrDefault(c => c.Type == "tenant_id").Value);
            Tenant = await dbContext
                .Tenants
                .FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

            if (Tenant == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Users = await dbContext.Users
                .Where(u => u.TenantId.Equals(tenantId))
                .ToListAsync();

            return Page();

        }
    }
}
