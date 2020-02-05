using MediatR;
using System;
using Testnt.Main.Application.Components.ProjectComponents.Common;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.Item
{
    public class GetProjectScenarioItemQuery : ProjectComponentRequest, IRequest<GetProjectScenarioItemDto>
    {
        public Guid Id { get; set; }
    }
}


