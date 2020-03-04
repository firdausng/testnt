using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Testnt.Idp.Domain.Entities;
using Testnt.Idp.Infra.Data;
using Testnt.Idp.Infra.Data.Seed;
using Testnt.Idp.Infra.Services.Email;

namespace Testnt.Idp.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            // seed class
            services.AddTransient<Config>();
            services.AddTransient<Users>();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(DependencyInjection).Assembly.GetName().Name;

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
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<TestntIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<TenantUserClaimsPrincipalFactory>();


            // setup dummy data
            services.AddTransient<IEmailSender, DummyEmailSender>();
            services.Configure<DummyAuthMessageSenderOptions>(configuration);

            return services;
        }
    }
}
