using Microsoft.Extensions.DependencyInjection;
using PrepareToInterview.Application.Services;
using System.Reflection;

namespace PrepareToInterview.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Remove MediatR since we're not using CQRS anymore
            // services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly));
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            // Register our services
            services.AddScoped<IQuestionService, QuestionService>();
        }
    }
}