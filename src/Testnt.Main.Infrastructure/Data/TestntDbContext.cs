using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testnt.Common.Interface;
using Testnt.Main.Domain.Entity;
using Testnt.Main.Domain.Entity.TestSessionEntity;

namespace Testnt.Main.Infrastructure.Data
{
    public class TestntDbContext : DbContext
    {
        private readonly ICurrentUserService currentUserService;

        public TestntDbContext(DbContextOptions<TestntDbContext> options, ICurrentUserService currentUserService)
           : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<TestProject> Projects { get; set; }
        public DbSet<TestOutline> TestOutlines { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestScenario> TestScenarios { get; set; }
        public DbSet<TestFeature> TestFeatures { get; set; }
        public DbSet<TestSession> TestSessions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Tag> TestTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<TestCase>().HasBaseType<TestOutline>();
            modelBuilder.Entity<TestScenario>().HasBaseType<TestOutline>();

            modelBuilder.Entity<TestProject>()
                .HasIndex(u => u.Name)
                .IsUnique();


            // many to many mapping for test outline and tags
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

            // reference https://haacked.com/archive/2019/07/29/query-filter-by-interface/
            // Note the .Where(t => t.BaseType == null) clause here. 
            // Query filters may only be applied to the root entity type of an inheritance hierarchy. 
            // This clause ensures we don’t try to apply a filter on a non-root type.
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => t.BaseType == null)
                .Where(t => t.IsOwned() == false)
                ;

            // add tenant id filter in all queries
            foreach (var entityType in entityTypes)
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.SetTenantIdFilter(entityType.ClrType, currentUserService);
                }
            }

   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            return base.Add(entity);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }
    }


    /// <summary>
    /// reference https://stackoverflow.com/questions/45096799/filter-all-queries-trying-to-achieve-soft-delete/45097532#45097532
    /// </summary>
    public static class EFFilterExtensions
    {
        public static void SetTenantIdFilter(this ModelBuilder modelBuilder, Type entityType, ICurrentUserService currentUserService)
        {
            SetTenantIdFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder, currentUserService });
        }

        static readonly MethodInfo SetTenantIdFilterMethod = typeof(EFFilterExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetTenantIdFilter");

        public static void SetTenantIdFilter<TEntity>(this ModelBuilder modelBuilder, ICurrentUserService currentUserService)
            where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => x.TenantId.Equals(currentUserService.TenantId));
        }
    }
}
