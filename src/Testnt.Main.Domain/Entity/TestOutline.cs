using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestOutline : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TestProjectId { get; set; }
        public TestProject TestProject { get; set; }
        public TestOutlineStatus Status { get; set; }
        public TestExecutionResult TestExecutionResult { get; set; }
        public List<TestTag> TestTags { get; set; } = new List<TestTag>();
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
