using FluentValidation;
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
            app.MapControllers();
            app.Run();
        }

    }
}
