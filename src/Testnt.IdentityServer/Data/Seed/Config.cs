// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Data.Seed
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name= "Tenant",
                    UserClaims = {"tenant_id", "organization_id"},
                    Required = true
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("testnt.main.api", "Testnt Rest API")
                {
                    UserClaims = new[]
                    {
                        "email",
                        "tenant_id"
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
            
            // resource owner password grant client
            // for test api only, disable in prod
            new Client
            {
                ClientName = "Testnt test client",
                ClientId = "testnt.main.test.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = 
                { 
                    // API Resources
                    "testnt.main.api"
                }
            },
            // machine to machine client
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes =
                { 
                    // IdentityResource
                    "Tenant",

                    // API Resources
                    "testnt.main.api"
                }
            },
             // JavaScript Client
            new Client
                {

                    ClientName = "Testnt Web Client",
                    ClientId = "testnt.main.spa.client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RedirectUris = new List<string>()
                    {
                        "https://localhost:7001/signin-oidc",
                        "http://localhost:7000/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:7001/signout-callback-oidc",
                        "http://localhost:7000/signout-callback-oidc",
                    },
                    AllowedScopes =
                    {
                        // IdentityResource
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Tenant",

                        // API Resources
                        "testnt.main.api"

                    },

                }
            };
        }

        public static void EnsureSeedData(IServiceScope serviceScope)
        {
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                Console.WriteLine("Adding Client operation");
                foreach (var client in GetClients())
                {
                    Console.WriteLine($"Adding {client.ToEntity().ClientName}");
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("Adding Identity resource operation");
                foreach (var resource in GetIdentityResources())
                {
                    Console.WriteLine($"Adding {resource.ToEntity().DisplayName}");
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("Adding API resource operation");
                foreach (var resource in Config.GetApis())
                {
                    Console.WriteLine($"Adding {resource.ToEntity().DisplayName}");
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}