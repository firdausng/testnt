using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestScenarios.Item.Query.GetTestScenarioItem
{
    public class GetTestScenarioItemQuery : BaseRequest, IRequest<GetTestScenarioItemDto>
    {
        public Guid Id { get; set; }
    }
}


