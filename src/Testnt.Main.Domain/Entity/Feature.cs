using System;
using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity
{
    public class Feature: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public List<Scenario> Scenarios { get; set; } = new List<Scenario>();
    }
}
