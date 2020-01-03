using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Application.TestCases.Item.Query.GetTestCaseItem
{
    public class GetTestCaseItemQuery: IRequest<GetTestCaseItemDto>
    {
        public Guid Id { get; set; }
    }
}
