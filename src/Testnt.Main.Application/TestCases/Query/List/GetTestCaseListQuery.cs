using MediatR;
using System;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestCases.Query.List
{
    public class GetTestCaseListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestCaseListDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
