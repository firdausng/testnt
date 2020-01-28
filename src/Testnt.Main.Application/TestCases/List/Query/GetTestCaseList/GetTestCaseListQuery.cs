using MediatR;
using System;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestCases.List.Query.GetTestCaseList
{
    public class GetTestCaseListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestCaseListDto>>
    {
        public GetTestCaseListQuery(Guid tenantId) 
        {
            this.TenantId = tenantId;
        }
    }
}
