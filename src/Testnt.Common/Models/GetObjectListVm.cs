using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Common.Models
{
    public class GetObjectListVm<T>
    {
        public IList<T> Data { get; set; }
        public int Count { get; set; }
    }
}
