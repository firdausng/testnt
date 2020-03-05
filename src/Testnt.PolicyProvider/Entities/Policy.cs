using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.PolicyProvider.Entities
{
    /// <summary>
    /// Models a policy
    /// </summary>
    public class Policy: BaseEntity
    {
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<Role> Roles { get; internal set; } = new List<Role>();

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public List<Permission> Permissions { get; internal set; } = new List<Permission>();
    }
}
