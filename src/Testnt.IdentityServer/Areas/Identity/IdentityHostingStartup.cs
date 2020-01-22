using System;
using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(Testnt.IdentityServer.Areas.Identity.IdentityHostingStartup))]
namespace Testnt.IdentityServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}