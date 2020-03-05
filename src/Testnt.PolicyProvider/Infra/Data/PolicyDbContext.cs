using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.PolicyProvider.Entities;

namespace Testnt.PolicyProvider.Infra.Data
{
    public class PolicyDbContext : DbContext
    {
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public PolicyDbContext(DbContextOptions<PolicyDbContext> options)
           : base(options)
        {
        }
    }
}
