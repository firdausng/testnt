using MediatR;
using System;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestSessions.Query.List
{
    public class GetTestSessionListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestSessionListDto>>
    {
    }
}
