using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace PrepareToInterview.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}