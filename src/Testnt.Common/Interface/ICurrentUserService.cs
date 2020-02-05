using System;

namespace Testnt.Common.Interface
{
    /// <summary>
    /// This is to get current user information
    /// because user is managed by identityserver
    /// </summary>
    public interface ICurrentUserService
    {
        Guid TenantId { get; set; }
        public string Name { get; }
        public string Email { get; }
    }
}
