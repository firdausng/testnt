using MediatR;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestCases.List.Query.GetTestCaseList
{
    public class GetTestCaseListQuery : IRequest<GetObjectListVm<GetTestCaseListDto>>
    {
    }
}
