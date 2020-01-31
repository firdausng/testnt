using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class Tag: BaseEntity
    {
        public Tag()
        {
        }
        public string Name { get; set; }
        public List<TestTag> TestTags { get; set; } = new List<TestTag>();
        public Guid ProjectId { get; set; }
    }
}
