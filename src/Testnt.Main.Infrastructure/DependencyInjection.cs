using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testnt.Common.Interface;
using Testnt.Main.Infrastructure.Data;
using Testnt.Main.Infrastructure.Services;

namespace Testnt.Main.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(DependencyInjection).Assembly.GetName().Name;
            services.AddDbContext<TestntDbContext>(cfg =>
            {
                cfg.UseNpgsql(configuration.GetConnectionString("PostgresTestntMainConnectionString"),
                    options =>
                    {
                        options.EnableRetryOnFailure(3);
                        options.MigrationsAssembly(migrationsAssembly);
                    });
            });

            services.AddScoped<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}
