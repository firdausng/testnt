using System;
using System.Collections.Generic;
using Testnt.Main.Domain.Entity.Projects;

namespace Testnt.Main.Domain.Entity.TestSessionEntity.Projects
{
    public class ScenarioSnapshot : ProjectComponentEntity
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
