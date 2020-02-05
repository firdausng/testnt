using MediatR;
using Testnt.Main.Application.Common;
using Testnt.Main.Application.Components.ProjectComponents.Common;

namespace Testnt.Main.Application.Components.ProjectComponents.Scenarios.Query.List
{
    public class GetProjectScenarioListQuery : ProjectComponentRequest, IRequest<GetObjectListVm<GetProjectScenarioListDto>>
    {
    }
}
