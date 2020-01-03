using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Testnt.Common.Interface;
using Testnt.Common.Mappings;
using Testnt.Main.Api.Rest.Middleware;
using Testnt.Main.Api.Rest.Services;
using Testnt.Main.Application;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Api.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("testnt.Main.Application");
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var applicationAssemblies = allAssemblies.Where(a => a.GetName().Name.StartsWith("testnt.Main.Application")).ToArray();

            services.AddApplication();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            services.AddMiniProfiler().AddEntityFramework();
            services.AddHealthChecks();
            AddAutoMapper(services);

            services.AddDbContext<TestntDbContext>(cfg =>
            {
                cfg.UseNpgsql(Configuration.GetConnectionString("PostgresTestntMainConnectionString"),
                    options =>
                    {
                        options.EnableRetryOnFailure(3);
                    });
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddFeatureFolders()
                ;

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;

                options.Audience = "testnt.main.api";
            });

            // not using FluentValidation.AspNetCore package due to issue - https://github.com/JasonGT/NorthwindTraders/issues/76
            // manually register fluent validation
            services.AddValidatorsFromAssemblies(applicationAssemblies);

            services.AddMediatR(assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
