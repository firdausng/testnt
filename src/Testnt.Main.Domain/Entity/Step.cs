
using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity
{
    public class Step: ProjectComponentEntity
    {
        public int Order { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<ScenarioStep> ScenarioSteps { get; set; } = new List<ScenarioStep>();
    }
}
