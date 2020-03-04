using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Testnt.IdentityServer.Common.Attribute;
using Testnt.Idp.App.Admin.Account.Query.List;
using Testnt.Idp.Domain.Entities;

namespace Testnt.IdentityServer.Areas.Admin.Pages.Account
{
    [SecurityHeaders]
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public GetObjectListWithTenantVm<GetAccountListVm> AccountlistWithTenantVm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            var tenantId = Guid.Parse(userClaims.FirstOrDefault(c => c.Type == "tenant_id").Value);
            AccountlistWithTenantVm = await mediator.Send(new GetAccountListQuery(tenantId));

            if (AccountlistWithTenantVm == null)
            {
                return RedirectToPage("/Account/Login");
            }
            //Tenant = await dbContext
            //    .Tenants
            //    .FirstOrDefaultAsync(t => t.Id.Equals(tenantId));

            //if (Tenant == null)
            //{
            //    return RedirectToPage("/Account/Login");
            //}

            //Users = await dbContext.Users
            //    .Where(u => u.TenantId.Equals(tenantId))
            //    .ToListAsync();

            return Page();

        }
    }
}
