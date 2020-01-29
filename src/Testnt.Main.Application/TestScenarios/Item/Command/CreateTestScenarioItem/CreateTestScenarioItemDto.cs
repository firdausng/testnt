using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Testnt.Main.Application.TestScenarios.Item.Command.CreateTestScenarioItem
{
    public class CreateTestScenarioItemDto : IMapFrom<TestScenario>
    {
        public Guid Id { get; set; }
    }
}
