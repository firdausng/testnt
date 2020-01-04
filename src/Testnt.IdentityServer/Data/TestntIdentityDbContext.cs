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
            base.OnModelCreating(modelBuilder);
        }
    }
}
