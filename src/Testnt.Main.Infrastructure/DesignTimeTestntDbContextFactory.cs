using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Testnt.Common.Interface;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Infrastructure
{
    public class DesignTimeTestntDbContextFactory : IDesignTimeDbContextFactory<TestntDbContext>
    {
        public TestntDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../Testnt.Main.Api.Rest/appsettings.Development.json")
                .Build();
            var builder = new DbContextOptionsBuilder<TestntDbContext>();
            var connectionString = configuration.GetConnectionString("PostgresTestntMainConnectionString");
            builder.UseNpgsql(connectionString);
            return new TestntDbContext(builder.Options, new DesignTimeCurrentUserService(), new DesignTimeDateTimeService());
        }

        internal class DesignTimeCurrentUserService : ICurrentUserService
        {
            public Guid TenantId { get; set; } = Guid.NewGuid();

            public string Name { get; set; } = "Design Time";

            public string Email { get; set; } = "designtime@email.com";
        }

        internal class DesignTimeDateTimeService : IDateTimeService
        {
            public DateTime Now => DateTime.Now;
        }
    }
}
