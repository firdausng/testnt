using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity.TestSessionEntity
{
    public class Session: BaseEntity
    {
        public string Name { get; set; }
        //public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Finished { get; set; }
        public List<ScenarioSnapshot> ScenarioSnapshot { get; set; }
    }
}
