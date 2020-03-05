using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Testnt.PolicyProvider.Infra.Data;

namespace Testnt.PolicyProvider
{
    /// <summary>
    /// PolicyServer client
    /// </summary>
    public class PolicyServerRuntimeClient : IPolicyServerRuntimeClient
    {
        private readonly PolicyDbContext policyDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyServerRuntimeClient"/> class.
        /// </summary>
        /// <param name="policy">The policy.</param>
        public PolicyServerRuntimeClient(PolicyDbContext policyDbContext)
        {
            this.policyDbContext = policyDbContext;
        }

        /// <summary>
        /// Determines whether the user is in a role.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(ClaimsPrincipal user, string role)
        {
            var policy = await EvaluateAsync(user);
            return policyDbContext.Roles.Any(r => r.Name.Equals(role));
        }

        /// <summary>
        /// Determines whether the user has a permission.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission)
        {
            var policy = await EvaluateAsync(user);
            return policyDbContext.Permissions.Any(p => p.Name.Equals(permission));
        }

        /// <summary>
        /// Evaluates the policy for a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">user</exception>
        public Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var roles = policyDbContext.Roles.Where(x => x.Evaluate(user)).Select(x => x.Name).ToArray();
            var permissions = policyDbContext.Permissions.Where(x => x.Evaluate(roles)).Select(x => x.Name).ToArray();

            var result = new PolicyResult()
            {
                Roles = roles.Distinct(),
                Permissions = permissions.Distinct()
            };

            return Task.FromResult(result);

        }
    }
}
