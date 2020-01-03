using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestScenario : TestOutline
    {
        public List<TestCase> TestCases { get; set; } = new List<TestCase>();
    }
}
