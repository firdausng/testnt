using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testnt.IdentityServer.Data.Entity;

namespace Testnt.IdentityServer.Data
{
    public class TestntIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public TestntIdentityDbContext(DbContextOptions<TestntIdentityDbContext> options)
        : base(options)
        { }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<ApplicationUser>();

            user.Property(u => u.UserName).IsRequired().HasMaxLength(256);
            user.HasIndex(u => u.TenantId);

            user.Property(u => u.TenantId)
                .IsRequired();

            var tenant = modelBuilder.Entity<Tenant>();
            tenant.HasIndex(u => u.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
