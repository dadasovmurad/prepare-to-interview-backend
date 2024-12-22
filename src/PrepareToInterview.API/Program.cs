using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using PrepareToInterview.API.Middlewares;
using PrepareToInterview.Application;
using PrepareToInterview.Application.Validators;
using PrepareToInterview.Persistence;

namespace PrepareToInterview.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var services = builder.Services;

            services.AddControllers();

            services.AddPersistenceServices();
            services.AddApplicationServices();
            services.AddControllersWithViews();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddValidatorsFromAssemblyContaining<CreateQuestionValidator>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
