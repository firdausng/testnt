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

        public DbSet<Project> Projects { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasIndex(u => u.Name);

            // many to many mapping for test outline and tags
            modelBuilder.Entity<TagLink>()
                .HasKey(bc => new { bc.ScenarioId, bc.TagId });
            modelBuilder.Entity<TagLink>()
                .HasOne(bc => bc.Scenario)
                .WithMany(b => b.Tags)
                .HasForeignKey(bc => bc.TagId);
            modelBuilder.Entity<TagLink>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.TagLinks)
                .HasForeignKey(bc => bc.ScenarioId);


            // many to many mapping for test project and user
            modelBuilder.Entity<ProjectUser>()
                .HasKey(bc => new { bc.ProjectId, bc.UserProfileId });
            modelBuilder.Entity<ProjectUser>()
                .HasOne(bc => bc.UserProfile)
                .WithMany(b => b.Projects)
                .HasForeignKey(bc => bc.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(bc => bc.Project)
                .WithMany(c => c.Members)
                .HasForeignKey(bc => bc.UserProfileId);


            modelBuilder.Entity<Scenario>()
                .OwnsMany(p => p.Steps);

            // reference https://haacked.com/archive/2019/07/29/query-filter-by-interface/
            // Note the .Where(t => t.BaseType == null) clause here. 
            // Query filters may only be applied to the root entity type of an inheritance hierarchy. 
            // This clause ensures we don’t try to apply a filter on a non-root type.
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => t.BaseType == null)
                .Where(t => t.IsOwned() == false);

            // add tenant id and index in all queries
            foreach (var entityType in entityTypes)
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.SetTenantIdFilterAndIndex(entityType.ClrType, currentUserService);
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.TenantId != currentUserService.TenantId)
                        {
                            entry.Entity.TenantId = currentUserService.TenantId;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity.TenantId != currentUserService.TenantId)
                        {
                            entry.Entity.TenantId = currentUserService.TenantId;
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            entity = InjectTenantId(entity);
            return base.Add(entity);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity = InjectTenantId(entity);
            return base.AddAsync(entity, cancellationToken);
        }

        private TEntity InjectTenantId<TEntity>(TEntity entity)
        {
            var tenantIdProp = typeof(TEntity).GetProperty("TenantId");
            tenantIdProp.SetValue(entity, currentUserService.TenantId);
            return entity;
        }
    }

    /// <summary>
    /// reference https://stackoverflow.com/questions/45096799/filter-all-queries-trying-to-achieve-soft-delete/45097532#45097532
    /// </summary>
    public static class EFFilterExtensions
    {
        public static void SetTenantIdFilterAndIndex(this ModelBuilder modelBuilder, Type entityType, ICurrentUserService currentUserService)
        {
            SetTenantIdFilterAndIndexMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder, currentUserService });
        }

        static readonly MethodInfo SetTenantIdFilterAndIndexMethod = typeof(EFFilterExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "SetTenantIdFilter");

        public static void SetTenantIdFilterAndIndex<TEntity>(this ModelBuilder modelBuilder, ICurrentUserService currentUserService)
            where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => x.TenantId.Equals(currentUserService.TenantId));
            modelBuilder.Entity<TEntity>().HasIndex(b => b.TenantId);
            //modelBuilder.Entity<TEntity>().;
        }
    }
}
