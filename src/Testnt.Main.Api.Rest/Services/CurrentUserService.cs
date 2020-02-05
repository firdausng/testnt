using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using Testnt.Common.Interface;

namespace Testnt.Main.Api.Rest.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var identity = httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            var tenantIdClaim = userClaims.FirstOrDefault(c => c.Type == "tenant_id");

            TenantId = Guid.Parse(tenantIdClaim.Value);
            Name = userClaims.FirstOrDefault(c => c.Type == "name").Value;
            Email = userClaims.FirstOrDefault(c => c.Type == "email").Value;
        }

        public Guid TenantId { get; set; }
        public string Name { get; }
        public string Email { get; }

    }

    public class MockCurrentUserService : ICurrentUserService
    {
        public MockCurrentUserService()
        {
            TenantId = Guid.NewGuid();
            Name = "mock";
            Email = "mock@email.com";
        }

        public Guid TenantId { get; set; }
        public string Name { get; }
        public string Email { get; }

    }
}
