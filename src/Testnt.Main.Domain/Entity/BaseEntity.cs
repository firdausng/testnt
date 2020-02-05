using System;

namespace Testnt.Main.Domain.Entity
{
    public abstract class BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
