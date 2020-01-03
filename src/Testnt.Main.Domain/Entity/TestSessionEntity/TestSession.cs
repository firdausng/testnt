using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity.TestSessionEntity
{
    public class TestSession: BaseEntity
    {
        public string Name { get; set; }
        //public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
        public Guid TestProjectId { get; set; }
        public TestProject TestProject { get; set; }
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Finished { get; set; }
        public List<TestScenarioSnapshot> TestScenarioSnapshot { get; set; }
    }
}
