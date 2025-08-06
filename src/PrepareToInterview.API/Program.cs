using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PrepareToInterview.API.Middlewares;
using PrepareToInterview.Application;
using PrepareToInterview.Application.Validators;
using PrepareToInterview.Persistence;
using PrepareToInterview.Persistence.Contexts;

namespace PrepareToInterview.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Angular dev server
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Add services to the container
            var services = builder.Services;

            services.AddControllers();
            services.AddPersistenceServices();
            services.AddApplicationServices();
            services.AddControllersWithViews();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddValidatorsFromAssemblyContaining<CreateQuestionValidator>();

            // Add Swagger services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var app = builder.Build();
        if (app.Environment.IsDevelopment())
{
    var application = app.Services.CreateScope().ServiceProvider.GetRequiredService<PrepareToInterviewAPIDbContext>();

    var pendingMigrations = await application.Database.GetPendingMigrationsAsync();
    if (pendingMigrations != null)
        await application.Database.MigrateAsync();
}

            // Enable Swagger middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty; // Swagger UI will be at the root URL
                });
            }

            app.UseCors("AllowAngularApp");

            // Configure the HTTP request pipeline
            app.UseExceptionHandler();
            
            // Enable static file serving for uploaded images
            app.UseStaticFiles();
            
            app.MapControllers();
            await app.RunAsync();
        }
    }
}
