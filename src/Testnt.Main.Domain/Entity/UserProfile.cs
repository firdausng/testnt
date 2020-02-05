using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity
{
    public class UserProfile: AuditableEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<ProjectUser> Projects { get; set; } = new List<ProjectUser>();
    }
}