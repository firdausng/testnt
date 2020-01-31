using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestScenarios.Query.Item
{
    public class GetTestScenarioItemQuery : IRequest<GetTestScenarioItemDto>
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }
}


