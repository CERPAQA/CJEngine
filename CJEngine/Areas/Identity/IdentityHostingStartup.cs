using System;
using CJEngine.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CJEngine.Areas.Identity.IdentityHostingStartup))]
namespace CJEngine.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CJEngineLoginContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CJEngineContextConnection")));
                //TODO: when this line and a similar in startup is applied application crashes, not sure why
                /*services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<CJEngineLoginContext>();*/
            });
        }
    }
}