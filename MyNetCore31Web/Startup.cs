using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication;
using MyNetCore31Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Unicode;
using System.Text;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using MyNetCore31Web.middleware;

namespace MyNetCore31Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //
            //services.AddDbContext<MyNetCore31WebContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyNetCore31WebContextConnection")));
            services.AddDbContext<MyNetCore31WebContext>(options => options.UseMySql(Configuration.GetConnectionString("MyNetCore31WebContextConnection")));

            services.AddMyCustomService(option => option.UseMySql(""));

            services.AddLocalization();
            //services.AddSingleton<IStringLocalizer, LocalserviceProvider>();
            services.AddSingleton<IStringLocalizer>((sp) =>
             {
                 var sx = sp.GetRequiredService<IStringLocalizer<LocalserviceProvider>>();
                 return sx;
             });

            services.AddSingleton<Jwtextension>();

            services.AddAuthentication(option =>
            {
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }
           ).AddJwtBearer(option =>
           {
               option.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = "123",
                   ValidateAudience = true,
                   ValidAudience = "4456",
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mimamimamimamimamimamimamima")),
                   ValidateLifetime = true,
                   RequireExpirationTime = true,
                   ClockSkew = TimeSpan.Zero,
               };

           });

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyNetCore31WebContext>();

            services.AddRazorPages();
            services.AddControllersWithViews().AddDataAnnotationsLocalization(op =>
            {
                op.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(LocalserviceProvider));
                };
            });

            //services.AddScoped<MyCustomMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //1¡¢app.Use(async (content,next) => { await content.Response.WriteAsync("1111");});
            //2¡¢app.Use(async (content, next) =>
            //{
            //    await content.Response.WriteAsync("1111");
            //    await next();
            //    await content.Response.WriteAsync("112444");
            //});
            //3¡¢app.Run(a => a.Response.WriteAsync("222222"));
            //app.UseMiddleware<MyCustomMiddleware>();
            app.UseMyMiddleware();
            app.Run(a => a.Response.WriteAsync("222222"));
            //app.UseMiddleware(typeof(MyMiddleware));
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                //DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("zh-Hans", "zh-Hans"),
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en", "en"),
                SupportedCultures = new List<CultureInfo>() { new CultureInfo("zh-Hans"), new CultureInfo("zh-Hant"), new CultureInfo("en") },
                SupportedUICultures = new List<CultureInfo>() { new CultureInfo("zh-Hans"), new CultureInfo("zh-Hant"), new CultureInfo("en") }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
