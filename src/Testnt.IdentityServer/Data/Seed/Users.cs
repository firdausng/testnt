// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testnt.IdentityServer.Data;
using Testnt.IdentityServer.Entities;

namespace IdentityServer.Data.Seed
{
    public class Users
    {
        public static void EnsureSeedData(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<TestntIdentityDbContext>();
            context.Database.Migrate();

            var mainTenant = context.Tenants.Where(t => t.Name.Equals("Testnt")).FirstOrDefaultAsync().Result;
            if (mainTenant == null)
            {
                mainTenant = new Tenant
                {
                    Name = "Testnt",
                   
                };
                context.Tenants.Add(mainTenant);
                context.SaveChanges();
            }

            

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var alice = userMgr.FindByNameAsync("alice").Result;
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
                var result = userMgr.CreateAsync(alice, "Password@01").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
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
                Console.WriteLine("alice created");
            }
            else
            {
                Console.WriteLine("alice already exists");
            }

            var bob = userMgr.FindByNameAsync("bob").Result;
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
                var result = userMgr.CreateAsync(bob, "Password@01").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
  
                result = userMgr.AddClaimsAsync(bob, new Claim[]{
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
                Console.WriteLine("bob created");
            }
            else
            {
                Console.WriteLine("bob already exists");
            }


            var demoTenant = context.Tenants.Where(t => t.Name.Equals("demoTenant")).FirstOrDefaultAsync().Result;
            if (demoTenant == null)
            {
                demoTenant = new Tenant
                {
                    Name = "demoTenant",

                };
                context.Tenants.Add(demoTenant);
                context.SaveChanges();
            }

            var kyle = userMgr.FindByNameAsync("kyle").Result;
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
                var result = userMgr.CreateAsync(kyle, "Password@01").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(kyle, new Claim[]{
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
                Console.WriteLine("kyle created");
            }
            else
            {
                Console.WriteLine("kyle already exists");
            }
        }
    }
}
