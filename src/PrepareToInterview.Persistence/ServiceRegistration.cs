using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Application.Services;
using PrepareToInterview.Persistence.Contexts;
using PrepareToInterview.Persistence.Repositories;
using PrepareToInterview.Persistence.Services;

namespace PrepareToInterview.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<PrepareToInterviewAPIDbContext>(options => options
                .UseNpgsql(Configuration.ConnectionString)
                .UseSnakeCaseNamingConvention());

            // Register repositories
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            // Register services
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITagService, TagService>();
        }
    }
}