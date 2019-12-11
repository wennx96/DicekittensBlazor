using System;
using DicekittensBlazor.Areas.Identity.Data;
using DicekittensBlazor.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DicekittensBlazor.Areas.Identity.IdentityHostingStartup))]
namespace DicekittensBlazor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DicekittensBlazorContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DicekittensBlazorContextConnection")));

                services.AddDefaultIdentity<DicekittensBlazorUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DicekittensBlazorContext>();
            });
        }
    }
}