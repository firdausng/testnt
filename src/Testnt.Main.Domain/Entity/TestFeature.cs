using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestFeature: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TestProjectId { get; set; }
        public TestProject TestProject { get; set; }
        public ICollection<TestScenario> TestScenarios { get; set; } = new List<TestScenario>();
    }
}
