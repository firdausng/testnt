using System;
using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity
{
    public class Feature: ProjectComponentEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
        public List<Scenario> Scenarios { get; set; } = new List<Scenario>();
    }
}
