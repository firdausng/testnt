using Testnt.Main.Domain.Entity.TestSessionEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class Project: BaseEntity
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public List<Scenario> Scenarios { get; set; } = new List<Scenario>();
        public List<Session> Sessions { get; set; } = new List<Session>();
        public List<Feature> Features { get; set; } = new List<Feature>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<ProjectUser> Members { get; set; } = new List<ProjectUser>();
    }
}
