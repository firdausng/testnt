using MediatR;
using System;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestSessions.List.Query.GetTestSessionList
{
    public class GetTestSessionListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestSessionListDto>>
    {
        public GetTestSessionListQuery(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}
