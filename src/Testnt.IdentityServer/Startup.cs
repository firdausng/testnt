// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer.Data.Seed;
using IdentityServer4.Configuration;
using IdentityServer4.Stores;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;
using Testnt.IdentityServer.Data;
using Testnt.IdentityServer.Infrastructure.Services.Email;
using IdentityServer4.EntityFramework.Stores;
using System.Security.Cryptography.X509Certificates;
using Testnt.IdentityServer.Entities;
using IdentityServer4;

namespace Testnt.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddData(Configuration);

            // setup dummy data
            services.AddTransient<IEmailSender, DummyEmailSender>();
            services.Configure<DummyAuthMessageSenderOptions>(Configuration);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(options =>
            {
                var clientList = Configuration.GetSection("Client:Ip").GetChildren().Select(s => s.Value).ToList();
                clientList.Add("http://localhost:4200");
                clientList.Add("http://127.0.0.1:4200");

                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            //.AllowCredentials()
                            .WithOrigins(clientList.ToArray())
                            //.SetIsOriginAllowedToAllowWildcardSubdomains()
                            //.SetIsOriginAllowed(isOriginAllowed: _ => true)
                            //.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var builder = services.AddIdentityServer(options =>
                {
                    //options.Events.RaiseErrorEvents = true;
                    //options.Events.RaiseInformationEvents = true;
                    //options.Events.RaiseFailureEvents = true;
                    //options.Events.RaiseSuccessEvents = true;

                    options.UserInteraction.LoginUrl = "/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Account/Logout";

                    options.IssuerUri = "http://testnt.identityserver";

                    options.Authentication = new AuthenticationOptions()
                    {
                        CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
                        CookieSlidingExpiration = true
                    };
                    
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                })
                //.AddCorsPolicyService()
                .AddAspNetIdentity<ApplicationUser>()
                //.AddSigningCredential(new X509Certificate2(Configuration.GetValue<string>("Certificate:Path"), "password"))
                ;


            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                    //options.
                })
                .AddFacebook(options => 
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "copy client ID from fb here";
                    options.ClientSecret = "copy client secret from fb here";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "copy client ID from microsoft here";
                    options.ClientSecret = "copy client secret from microsoft here";
                })
                ;
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            // With endpoint routing, the CORS middleware must be configured to execute between the calls to UseRouting and UseEndpoints. Incorrect configuration will cause the middleware to stop functioning correctly.
            app.UseCors("AllowAllOrigins");

            app.UseIdentityServer();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }



    }
}
