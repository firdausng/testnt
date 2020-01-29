using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestCases.Query.Item
{
    public class GetTestCaseItemQuery: BaseRequest, IRequest<GetTestCaseItemDto>
    {
        public Guid Id { get; set; }
    }
}
