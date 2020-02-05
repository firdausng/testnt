using System;
using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity.TestSessionEntity
{
    public class ScenarioSnapshot : BaseEntity
    {
        public Guid ScenarioId { get; set; }
        public string ScenarioName { get; set; }
        public TestExecutionResult ExecutionResult { get; set; }
        public List<StepSnapshot> StepSnapshot { get; set; }
    }

    public enum TestExecutionResult
    {
        Pass,
        Fail,
        Skip,
        Pending
    }
}
