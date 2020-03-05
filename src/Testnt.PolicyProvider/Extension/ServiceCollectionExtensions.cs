using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.PolicyProvider.Infra.Data;

namespace Testnt.PolicyProvider.Extension
{
    /// <summary>
    /// Helper class to configure DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the policy server client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static PolicyServerBuilder AddPolicyServerClient(this IServiceCollection services, Action<DbContextOptionsBuilder> cfg)
        {
            services.AddTransient<IPolicyServerRuntimeClient, PolicyServerRuntimeClient>();
            services.AddDbContext<PolicyDbContext>(cfg);

            return new PolicyServerBuilder(services);
        }
    }
}
