using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore31Web
{
    public class CustomBuilder
    {
        public IServiceCollection Services { get; set; }

        public CustomBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void UseSqlServer(string conn) {
            //Services.Add()
        }

        public void UseMySql(string conn)
        {

        }

        
    }
}
