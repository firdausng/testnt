using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestProjects.List.Query.GetTestProjectList
{
    public class GetTestProjectListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestProjectListDto>>
    {

    }
}
