using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Testnt.PolicyProvider.Entities
{
    /// <summary>
    /// Models an application role
    /// </summary>
    public class Role: BaseEntity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the subjects.
        /// </summary>
        /// <value>
        /// The subjects.
        /// </value>
        public List<string> Subjects { get; internal set; } = new List<string>();

        /// <summary>
        /// Gets the identity roles.
        /// </summary>
        /// <value>
        /// The identity roles.
        /// </value>
        public List<string> IdentityRoles { get; internal set; } = new List<string>();

        internal bool Evaluate(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var sub = user.FindFirst("sub")?.Value;
            if (!String.IsNullOrWhiteSpace(sub))
            {
                if (Subjects.Contains(sub)) return true;
            }

            var roles = user.FindAll("role").Select(x => x.Value);
            if (roles.Any())
            {
                if (IdentityRoles.Any(x => roles.Contains(x))) return true;
            }

            return false;
        }
    }
}
