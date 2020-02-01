using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.Components.ProjectComponents.Common;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.Item
{
    public class GetProjectScenarioItemQuery : ProjectComponentRequest, IRequest<GetProjectScenarioItemDto>
    {
        public Guid Id { get; set; }
    }
}


