using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config=>config.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}