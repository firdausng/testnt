using System;
using System.Collections.Generic;
using System.Text;

namespace Testnt.Common.Interface
{
    public interface ICurrentUserService
    {
        Guid TenantId { get; set; }
        public string Name { get; }
        public string Email { get; }
    }
}
