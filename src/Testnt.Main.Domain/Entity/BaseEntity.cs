using System;

namespace Testnt.Main.Domain.Entity
{
    public abstract class BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
    }
}
