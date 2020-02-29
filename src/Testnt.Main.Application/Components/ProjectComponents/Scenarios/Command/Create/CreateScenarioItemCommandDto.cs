using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using Testnt.Main.Domain.Entity.Projects;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Command.Item
{
    public class CreateScenarioItemCommandDto : IMapFrom<Scenario>
    {
        public Guid Id { get; set; }
    }
}
