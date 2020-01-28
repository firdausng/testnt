using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestCases.Item.Query.GetTestCaseItem
{
    public class GetTestCaseItemQuery: BaseRequest, IRequest<GetTestCaseItemDto>
    {
        public GetTestCaseItemQuery(Guid tenantId)
        {
            this.TenantId = tenantId;
        }

        public Guid Id { get; set; }
    }
}
