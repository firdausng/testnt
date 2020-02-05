using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity
{
    public class ProjectComponentEntity: AuditableEntity
    {
        public Guid ProjectId { get; set; }
    }
}
