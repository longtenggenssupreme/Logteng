using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAspnetCore.Cache;
using Microsoft.Extensions.Caching.Redis;

namespace TestAspnetCore
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
            services.AddControllersWithViews();
            services.AddSingleton<MyCustomCache>();

            //添加分布式缓存注册服务
            services.AddDistributedMemoryCache();

            ////添加分布式redis缓存注册服务
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "localhost:6379";//这个需要连接独立的redis服务器，redis服务需要启动
            //    option.InstanceName = "mycache";
            //});

            ////添加分布式sqlserver缓存注册服务
            //services.AddDistributedSqlServerCache(option =>
            //{
            //    option.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";//这个需要连接独立的sqlserver数据库
            //    option.DefaultSlidingExpiration = TimeSpan.FromSeconds(2);
            //    option.SchemaName = "dbo";
            //    option.TableName = "mysqlserverCache";
            //    option.SystemClock = new MySystemClock();
            //});
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
