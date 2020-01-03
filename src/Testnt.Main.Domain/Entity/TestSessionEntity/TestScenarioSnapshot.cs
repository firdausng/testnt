using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity.TestSessionEntity
{
    public class TestScenarioSnapshot : BaseEntity
    {
        public Guid TestScenarioId { get; set; }
        public string TestScenarioName { get; set; }
        public TestExecutionResult TestExecutionResult { get; set; }
        public List<TestCaseSnapshot> TestCaseSnapshots { get; set; } = new List<TestCaseSnapshot>();
    }

    public enum TestExecutionResult
    {
        Pass,
        Fail,
        Skip,
        Pending
    }
}
