using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class Tag: BaseEntity
    {
        public Tag()
        {
            TestTags = new List<TestTag>();
        }
        public string Name { get; set; }
        public ICollection<TestTag> TestTags { get; set; }
        public Guid ProjectId { get; set; }
    }
}
