using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Testnt.Idp.Infra.Data;

namespace Testnt.Idp.Infra
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestntIdentityDbContext>
    {
        public TestntIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../Testnt.IdentityServer/appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<TestntIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString);
            return new TestntIdentityDbContext(builder.Options);
        }
    }

    public class DesignTimePersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(DependencyInjection).Assembly.GetName().Name;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../Testnt.IdentityServer/appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure(3);
                options.MigrationsAssembly(migrationsAssembly);
            });
            var operationalStoreOptions = new OperationalStoreOptions
            {
            };
            return new PersistedGrantDbContext(builder.Options, operationalStoreOptions);
        }
    }

    public class DesignTimeConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(DependencyInjection).Assembly.GetName().Name;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../Testnt.IdentityServer/appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connectionString, options =>
            {
                options.EnableRetryOnFailure(3);
                options.MigrationsAssembly(migrationsAssembly);
            });
            var configurationStoreOptions = new ConfigurationStoreOptions
            {
            };
            return new ConfigurationDbContext(builder.Options, configurationStoreOptions);
        }
    }
}
