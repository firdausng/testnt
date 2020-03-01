// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Testnt.IdentityServer.Data;
using Testnt.IdentityServer.Entities;

namespace IdentityServer.Data.Seed
{
    public class Users
    {
        private readonly TestntIdentityDbContext testntIdentityDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ILogger<Users> logger;

        public Users(TestntIdentityDbContext testntIdentityDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILogger<Users> logger)
        {
            this.testntIdentityDbContext = testntIdentityDbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public void EnsureSeedData()
        {
            testntIdentityDbContext.Database.Migrate();

            var mainTenant = testntIdentityDbContext.Tenants.Where(t => t.Name.Equals("Testnt")).FirstOrDefaultAsync().Result;
            if (mainTenant == null)
            {
                logger.LogInformation("cannot find Testnt tenant");
                mainTenant = new Tenant
                {
                    Name = "Testnt",
                   
                };
                testntIdentityDbContext.Tenants.Add(mainTenant);
                testntIdentityDbContext.SaveChanges();
                logger.LogInformation("Testnt tenant created");
            }
            else
            {
                logger.LogInformation("Testnt tenant created");
            }

            var alice = userManager.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    TenantId = mainTenant.Id,
                    IsEnabled = true
                };
                var result = userManager.CreateAsync(alice, "Password@01").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(alice, new Claim[]{
                        new Claim("tenant_id", alice.TenantId.ToString()),
                        new Claim(JwtClaimTypes.ClientId, "testnt.main.web.client"),
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                logger.LogInformation("alice created");
            }
            else
            {
                logger.LogInformation("alice already exists");
            }

            var bob = userManager.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    TenantId = mainTenant.Id,
                    IsEnabled = true
                };
                var result = userManager.CreateAsync(bob, "Password@01").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
  
                result = userManager.AddClaimsAsync(bob, new Claim[]{
                        new Claim("tenant_id", bob.TenantId.ToString()),
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                logger.LogInformation("bob created");
            }
            else
            {
                logger.LogInformation("bob already exists");
            }


            var demoTenant = testntIdentityDbContext.Tenants.Where(t => t.Name.Equals("demoTenant")).FirstOrDefaultAsync().Result;
            if (demoTenant == null)
            {
                demoTenant = new Tenant
                {
                    Name = "demoTenant",

                };
                testntIdentityDbContext.Tenants.Add(demoTenant);
                testntIdentityDbContext.SaveChanges();
            }

            var kyle = userManager.FindByNameAsync("kyle").Result;
            if (kyle == null)
            {
                kyle = new ApplicationUser
                {
                    UserName = "kyle",
                    Email = "kylejohn@email.com",
                    EmailConfirmed = true,
                    TenantId = demoTenant.Id,
                    IsEnabled = true
                };
                var result = userManager.CreateAsync(kyle, "Password@01").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(kyle, new Claim[]{
                        new Claim("tenant_id", kyle.TenantId.ToString()),
                        new Claim(JwtClaimTypes.ClientId, "testnt.main.web.client"),
                        new Claim(JwtClaimTypes.Name, "Kyle John"),
                        new Claim(JwtClaimTypes.GivenName, "Kyle"),
                        new Claim(JwtClaimTypes.FamilyName, "John"),
                        new Claim(JwtClaimTypes.Email, "kylejohn@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                    }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                logger.LogInformation("kyle created");
            }
            else
            {
                logger.LogInformation("kyle already exists");
            }
        }
    }
}
