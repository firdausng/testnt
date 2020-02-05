using System;
using System.Collections.Generic;

namespace Testnt.Main.Domain.Entity
{
    public class Tag: AuditableEntity
    {
        public Tag()
        {
        }
        public string Name { get; set; }
        public List<TagLink> TagLinks { get; set; } = new List<TagLink>();
        public Guid ProjectId { get; set; }
    }
}
