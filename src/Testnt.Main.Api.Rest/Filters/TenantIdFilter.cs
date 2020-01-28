using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Api.Rest.Filters
{
    public class TenantIdFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            var tenantIdClaim = identity.Claims.FirstOrDefault(c => c.Type == "tenant_id");
            if(tenantIdClaim == null)
            {
                throw new ArgumentNullException("tenant_id");
            }
            
        }
    }
}
