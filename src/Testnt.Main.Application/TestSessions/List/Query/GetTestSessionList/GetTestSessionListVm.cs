using System.Collections.Generic;

namespace Testnt.Main.Application.TestSessions.List.Query.GetTestSessionList
{
    public class GetTestSessionListVm
    {
        public IList<GetTestSessionListDto> Data { get; set; }

        public int Count { get; set; }
    }
}
