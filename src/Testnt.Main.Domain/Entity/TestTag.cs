using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class TestTag
    {
        public Guid TestOutlineId { get; set; }
        public TestOutline TestOutline { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
