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

            //��ӷֲ�ʽ����ע�����
            services.AddDistributedMemoryCache();

            ////��ӷֲ�ʽredis����ע�����
            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = "localhost:6379";//�����Ҫ���Ӷ�����redis��������redis������Ҫ����
            //    option.InstanceName = "mycache";
            //});

            ////��ӷֲ�ʽsqlserver����ע�����
            //services.AddDistributedSqlServerCache(option =>
            //{
            //    option.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";//�����Ҫ���Ӷ�����sqlserver���ݿ�
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
