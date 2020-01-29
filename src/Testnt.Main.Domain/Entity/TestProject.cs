using Testnt.Main.Domain.Entity.TestSessionEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestProject: BaseEntity
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
        public ICollection<TestScenario> TestScenarios { get; set; } = new List<TestScenario>();
        public ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
        public ICollection<TestFeature> TestFeatures { get; set; } = new List<TestFeature>();
        public ICollection<Tag> TestTags { get; set; } = new List<Tag>();
        public ICollection<ProjectUser> Members { get; set; } = new List<ProjectUser>();
    }
}
