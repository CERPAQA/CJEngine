using System;
using CJEngine.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebPWrecover.Services;

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

                /*services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<CJEngineLoginContext>();*/

                /*services.AddIdentity<IdentityUser, IdentityRole>()
                   .AddEntityFrameworkStores<CJEngineLoginContext>();*/
                //.AddDefaultTokenProviders();
            });
        }
    }
}