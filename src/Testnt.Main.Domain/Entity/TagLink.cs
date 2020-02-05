using System;

namespace Testnt.Main.Domain.Entity
{
    public class TagLink
    {
        public Guid ScenarioId { get; set; }
        public Scenario Scenario { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
