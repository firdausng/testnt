using System.Collections.Generic;

namespace Testnt.Main.Application.TestCases.List.Query.GetTestCaseList
{
    public class GetTestCaseListVm
    {
        public IList<GetTestCaseListDto> TestCases { get; set; }

        public int Count { get; set; }
    }
}
