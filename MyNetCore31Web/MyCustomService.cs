using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetCore31Web
{
    public static class MyCustomService
    {
        public static IServiceCollection AddMyCustomService(this IServiceCollection services, Action<CustomBuilder> action)
        {
            CustomBuilder customBuilder = new CustomBuilder(services);
            action(customBuilder);
            return services;
        }
    }
}
