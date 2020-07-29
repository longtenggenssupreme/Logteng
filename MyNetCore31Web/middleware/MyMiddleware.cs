using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore31Web.middleware
{
    public static class MyMiddleware
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app) {

            //app.Use()
            //app.Use(async (content, next) => { await content.Response.WriteAsync("1111"); });
            //app.Use(async (content, next) =>
            //{
            //    await content.Response.WriteAsync("1111");
            //    await next();
            //    await content.Response.WriteAsync("112444");
            //});
            //3、app.Run(a => a.Response.WriteAsync("222222"));
            //app.UseMiddleware()
            app.UseMiddleware<MyCustomMiddleware>();
            return app;
        }
        
    }
}
