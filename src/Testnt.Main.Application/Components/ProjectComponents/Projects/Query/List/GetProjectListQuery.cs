using MediatR;
using Testnt.Common.Models;

namespace Testnt.Main.Application.Components.ProjectComponents.Projects.Query.List
{
    public class GetProjectListQuery : IRequest<GetObjectListVm<GetProjectListDto>>
    {

    }
}
