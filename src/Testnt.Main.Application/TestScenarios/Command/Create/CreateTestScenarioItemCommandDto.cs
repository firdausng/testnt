using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Testnt.Main.Application.TestScenarios.Command.Item
{
    public class CreateTestScenarioItemCommandDto : IMapFrom<Scenario>
    {
        public Guid Id { get; set; }
    }
}
