using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyNetCore31Web.Data;

[assembly: HostingStartup(typeof(MyNetCore31Web.Areas.Identity.IdentityHostingStartup))]
namespace MyNetCore31Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<MyNetCore31WebContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("MyNetCore31WebContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<MyNetCore31WebContext>();
            //});
        }
    }
}