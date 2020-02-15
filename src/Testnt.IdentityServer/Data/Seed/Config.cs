// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Data.Seed
{
    public class Config
    {
        private readonly ConfigurationDbContext configurationDbContext;
        private readonly PersistedGrantDbContext persistedGrantDbContext;
        private readonly IConfiguration configuration;
        private readonly ILogger<Config> logger;

        public Config(ConfigurationDbContext configurationDbContext, PersistedGrantDbContext persistedGrantDbContext, IConfiguration configuration, ILogger<Config> logger)
        {
            this.configurationDbContext = configurationDbContext;
            this.persistedGrantDbContext = persistedGrantDbContext;
            this.configuration = configuration;
            this.logger = logger;
        }
        public IEnumerable<IdentityResource> GetIdentityResources()
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

        public IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("testnt.main.api", "Testnt Rest API")
                {
                    UserClaims = new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Profile,
                        "tenant_id",
                    }
                }
            };
        }

        public IEnumerable<Client> GetClients(List<string> clientList)
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
                    RequirePkce = true,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RedirectUris = clientList,
                    PostLogoutRedirectUris = clientList,
                    AllowedCorsOrigins =clientList,

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

        public void EnsureSeedData()
        {
            persistedGrantDbContext.Database.Migrate();

            //var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            //var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
            //var logger = serviceScope.ServiceProvider.GetRequiredService<ILogging>();

            var clientList = configuration.GetSection("Client:Ip").GetChildren().Select(s => s.Value).ToList();

            configurationDbContext.Database.Migrate();

            if (!configurationDbContext.Clients.Any())
            {
                logger.LogInformation("Adding Client operation");

                foreach (var client in GetClients(clientList))
                {
                    logger.LogInformation($"Adding {client.ToEntity().ClientName}");
                    configurationDbContext.Clients.Add(client.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            if (!configurationDbContext.IdentityResources.Any())
            {
                logger.LogInformation("Adding Identity resource operation");
                foreach (var resource in GetIdentityResources())
                {
                    logger.LogInformation($"Adding {resource.ToEntity().DisplayName}");
                    configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            if (!configurationDbContext.ApiResources.Any())
            {
                logger.LogInformation("Adding API resource operation");
                foreach (var resource in GetApis())
                {
                    logger.LogInformation($"Adding {resource.ToEntity().DisplayName}");
                    configurationDbContext.ApiResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }
        }
    }
}