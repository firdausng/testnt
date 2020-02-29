using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Main.Domain.Entity.Projects
{
    public class ProjectComponentEntity: AuditableEntity
    {
        public Guid ProjectId { get; set; }
    }
}
