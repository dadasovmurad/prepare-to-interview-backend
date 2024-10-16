using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
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
            services.AddControllersWithViews();
            services.AddValidatorsFromAssemblyContaining<CreateQuestionValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
