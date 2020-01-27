using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Application.TestScenarios.List.Query
{
    public class GetTestScenarioListVm
    {
        public IList<GetTestScenarioListDto> Data { get; set; }

        public int Count { get; set; }
    }
}
