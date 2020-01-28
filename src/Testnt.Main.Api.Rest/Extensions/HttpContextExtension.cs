using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Testnt.Main.Api.Rest.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetTenantId(this HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var tenantIdClaim = identity.Claims.FirstOrDefault(c => c.Type == "tenant_id");
            var tenantId = Guid.Parse(tenantIdClaim.Value);
            return tenantId;
        }
    }
}
