using System;

namespace Testnt.Main.Domain.Entity
{
    public class ProjectUser
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
