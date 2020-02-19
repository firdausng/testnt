using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using Testnt.Common.Interface;

namespace Testnt.Main.Api.Rest.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILogger<CurrentUserService> logger)
        {
            // if HttpContext is null, it mean that ef core migration is running, so put mock data to ensure migration do not fail
            // this is hopefully just temporary solution, maybe should update dbcontext instead to handle this
            //if (httpContextAccessor.HttpContext == null && configuration.GetValue<bool>("Mock:DbContext")==true)
            if (httpContextAccessor.HttpContext == null)
            {
                logger.LogInformation("Failed to find httpcontext, creating mock {Service}", typeof(CurrentUserService));
                TenantId = Guid.NewGuid();
                Name = "mock";
                Email = "mock@email.com";
            }
            else
            {
                logger.LogInformation("HttpContext Found");
                var identity = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
                var userClaims = identity.Claims;
                var tenantIdClaim = userClaims.FirstOrDefault(c => c.Type == "tenant_id");


                TenantId = Guid.Parse(tenantIdClaim.Value);
                Name = userClaims.FirstOrDefault(c => c.Type == "name").Value;
                Email = userClaims.FirstOrDefault(c => c.Type == "email").Value;
            }
            logger.LogInformation("Assign Tenant Id: {TenantId}, Name: {Name}, Email:{Email}", TenantId, Name, Email);
        }

        public Guid TenantId { get; set; }
        public string Name { get; }
        public string Email { get; }

    }
}
