using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestScenarios.Item.Query
{
    public class GetTestScenarioItemQuery : BaseRequest, IRequest<GetTestScenarioItemDto>
    {
        public GetTestScenarioItemQuery(Guid tenantId)
        {
            TenantId = tenantId;
        }
        public Guid Id { get; set; }
    }
}


