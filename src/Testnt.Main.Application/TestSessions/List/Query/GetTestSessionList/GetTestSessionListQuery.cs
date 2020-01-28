using MediatR;
using System;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestSessions.List.Query.GetTestSessionList
{
    public class GetTestSessionListQuery : IRequest<GetObjectListVm<GetTestSessionListDto>>
    {
    }
}
