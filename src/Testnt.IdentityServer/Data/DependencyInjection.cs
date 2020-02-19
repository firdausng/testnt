using IdentityServer.Data.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Testnt.IdentityServer.Entities;

namespace Testnt.IdentityServer.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            // seed class
            services.AddTransient<Config>();
            services.AddTransient<Users>();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddTransient<IUserStore<ApplicationUser>, TestntUserStore>();
            services.AddDbContext<TestntIdentityDbContext>(cfg =>
            {
                cfg.UseNpgsql(connectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure(3);
                        options.MigrationsAssembly(migrationsAssembly);
                    });
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<TestntIdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
