using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Application.TestProjects.List.Query.GetTestProjectList
{
    public class GetTestProjectListVm
    {
        public IList<GetTestProjectListDto> Projects { get; set; }

        public int Count { get; set; }
    }
}
