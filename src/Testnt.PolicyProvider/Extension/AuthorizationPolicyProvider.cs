﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Testnt.PolicyProvider.Extension
{
    /// <summary>
    /// Authorization policy provider to automatically turn all permissions of a user into a ASP.NET Core authorization policy
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.DefaultAuthorizationPolicyProvider" />
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationPolicyProvider"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        /// <summary>
        /// Gets a <see cref="T:Microsoft.AspNetCore.Authorization.AuthorizationPolicy" /> from the given <paramref name="policyName" />
        /// </summary>
        /// <param name="policyName">The policy name to retrieve.</param>
        /// <returns>
        /// The named <see cref="T:Microsoft.AspNetCore.Authorization.AuthorizationPolicy" />.
        /// </returns>
        public async override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // check static policies first
            var policy = await base.GetPolicyAsync(policyName);

            if (policy == null)
            {
                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(policyName))
                    .Build();
            }

            return policy;
        }
    }

    internal class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }

    internal class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPolicyServerRuntimeClient _client;

        public PermissionHandler(IPolicyServerRuntimeClient client)
        {
            _client = client;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (await _client.HasPermissionAsync(context.User, requirement.Name))
            {
                context.Succeed(requirement);
            }
        }
    }
}
