using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testnt.PolicyProvider.Entities
{
    /// <summary>
    /// Models a permission
    /// </summary>
    public class Permission: BaseEntity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<string> Roles { get; internal set; } = new List<string>();

        internal bool Evaluate(IEnumerable<string> roles)
        {
            if (roles == null) throw new ArgumentNullException(nameof(roles));

            if (Roles.Any(x => roles.Contains(x))) return true;

            return false;
        }
    }
}
