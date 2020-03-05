using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.PolicyProvider.Entities
{
    public abstract class BaseEntity
    {
        public Guid TenantId { get; set; }
        public Guid Id { get; set; }
    }
}
