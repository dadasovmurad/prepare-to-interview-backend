using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;
using PrepareToInterview.Persistence.Repositories;


namespace PrepareToInterview.Persistence
{
    public static class ServiceRegistation
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            string conStr = Configuration.ConnectionString;
            services.AddDbContext<PrepareToInterviewAPIDbContext>(options => options
                                                                             .UseNpgsql(Configuration.ConnectionString)
                                                                             .UseSnakeCaseNamingConvention());

            services.AddScoped<IQuestionReadRepository, QuestionReadRepository>();
            services.AddScoped<IQuestionWriteRepository, QuestionWriteRepository>();

            services.AddScoped<IAnswerReadRepository, AnswerReadRepository>();
            services.AddScoped<IAnswerWriteRepository, AnswerWriteRepository>();

            services.AddScoped<ICommentReadRepository, CommentReadRepository>();
            services.AddScoped<ICommentReadRepository, CommentReadRepository>();

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<ITagWriteRepository, TagWriteRepository>();
            services.AddScoped<ITagReadRepository, TagReadRepository>();

            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            services.AddScoped<IContributionReadRepository, ContributionReadRepository>();
            services.AddScoped<IContributionWriteRepository, ContributionWriteRepository>();
        }
    }
}