using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.TestSessionEntity;

namespace Testnt.Main.Infrastructure.Data
{
    public class TestntDbContext : DbContext
    {
        public TestntDbContext(DbContextOptions<TestntDbContext> options)
           : base(options)
        { }

        public DbSet<TestProject> Projects { get; set; }
        public DbSet<TestOutline> TestOutlines { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestScenario> TestScenarios { get; set; }
        public DbSet<TestFeature> TestFeatures { get; set; }
        public DbSet<TestSession> TestSessions { get; set; }
        public DbSet<Tag> TestTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestCase>().HasBaseType<TestOutline>();
            modelBuilder.Entity<TestScenario>().HasBaseType<TestOutline>();

            modelBuilder.Entity<TestProject>()
                .HasIndex(u => u.Name)
                .IsUnique();


            // many to many mapping for test projects and tags
            modelBuilder.Entity<TestTag>()
                .HasKey(bc => new { bc.TestOutlineId, bc.TagId });
            modelBuilder.Entity<TestTag>()
                .HasOne(bc => bc.TestOutline)
                .WithMany(b => b.TestTags)
                .HasForeignKey(bc => bc.TagId);
            modelBuilder.Entity<TestTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.TestTags)
                .HasForeignKey(bc => bc.TestOutlineId);


            // many to many mapping for test project and user
            modelBuilder.Entity<ProjectUser>()
                .HasKey(bc => new { bc.ProjectId, bc.UserProfileId });
            modelBuilder.Entity<ProjectUser>()
                .HasOne(bc => bc.UserProfile)
                .WithMany(b => b.Projects)
                .HasForeignKey(bc => bc.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(bc => bc.TestProject)
                .WithMany(c => c.Members)
                .HasForeignKey(bc => bc.UserProfileId);


            modelBuilder.Entity<TestCase>()
                .OwnsMany(p => p.TestSteps);
        }
    }
}
