using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity.Projects
{
    public class ScenarioStep
    {
        public Guid ScenarioId { get; set; }
        public Scenario Scenario { get; set; }
        public Guid StepId { get; set; }
        public Step Step { get; set; }
    }
}
