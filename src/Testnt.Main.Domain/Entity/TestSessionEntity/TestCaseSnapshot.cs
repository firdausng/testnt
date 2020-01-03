using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity.TestSessionEntity
{
    public class TestCaseSnapshot: BaseEntity
    {
        public TestCaseSnapshot()
        {
            TestStepSnapshot = new List<TestStepSnapshot>();
        }
        public Guid TestCaseId { get; set; }
        public string TestCaseName { get; set; }

        public List<TestStepSnapshot> TestStepSnapshot { get; set; }
        public Guid TestScenarioSnapshotId { get; set; }
        public TestScenarioSnapshot TestScenarioSnapshot { get; set; }
    }
}
