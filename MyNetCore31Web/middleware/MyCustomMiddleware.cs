using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNetCore31Web.middleware
{
    public class MyCustomMiddleware
    {
        //RequestDelegate HttpContext
        public RequestDelegate Next { get; set; }

        public MyCustomMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        //public MyCustomMiddleware()
        //{
        //}
        public async Task InvokeAsync(HttpContext request)
        {
            await request.Response.WriteAsync("start....", Encoding.Default);
            //await Next(request);
            await request.Response.WriteAsync("end....", Encoding.Default);
        }
    }
}
