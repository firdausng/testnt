using MediatR;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Query.List
{
    public class GetProjectListQuery : IRequest<GetObjectListVm<GetProjectListDto>>
    {

    }
}
