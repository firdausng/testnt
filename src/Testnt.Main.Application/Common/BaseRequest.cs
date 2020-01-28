using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Testnt.Main.Application.Common
{
    public abstract class BaseRequest
    {
        public BaseRequest()
        {
        }

        public Guid TenantId { get; set; }

        public T AttachTenantId<T>(HttpContext httpContext, T request) where T:BaseRequest
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var tenantIdClaim = identity.Claims.FirstOrDefault(c => c.Type == "tenant_id");
            var tenantId = Guid.Parse(tenantIdClaim.Value);
            request.TenantId = tenantId;
            return request;
        }
    }
}
