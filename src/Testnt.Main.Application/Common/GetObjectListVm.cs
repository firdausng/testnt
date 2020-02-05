using System.Collections.Generic;

namespace Testnt.Main.Application.Common
{
    public class GetObjectListVm<T>
    {
        public IList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
