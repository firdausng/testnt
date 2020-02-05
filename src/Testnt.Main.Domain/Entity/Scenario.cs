using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity
{
    public class Scenario : ProjectComponentEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
        public TestOutlineStatus Status { get; set; }
        public TestExecutionResult TestExecutionResult { get; set; }
        public List<TagLink> Tags { get; set; } = new List<TagLink>();
        public List<Step> Steps { get; set; } = new List<Step>();
    }

    public enum TestOutlineStatus
    {
        Active,
        Draft,
        Archive
    }

    public enum TestExecutionResult
    {
        Pass,
        Fail,
        Skip,
        Pending
    }
}
