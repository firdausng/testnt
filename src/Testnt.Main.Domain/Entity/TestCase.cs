using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestCase: TestOutline
    {
        public TestCase()
        {
            TestSteps = new List<TestStep>();
        }
        public List<TestStep> TestSteps { get; set; }
        public Guid TestScenarioId { get; set; }
        public TestScenario TestScenario { get; set; }
    }

    
}
