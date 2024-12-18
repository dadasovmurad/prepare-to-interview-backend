using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Persistence.Contexts;
using PrepareToInterview.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrepareToInterview.Persistence
{
    public static class ServiceRegistation
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            string conStr = Configuration.ConnectionString;
            services.AddDbContext<PrepareToInterviewAPIDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

            services.AddScoped<IQuestionReadRepository, QuestionReadRepository>();
            services.AddScoped<IQuestionWriteRepository, QuestionWriteRepository>();

            services.AddScoped<IAnswerReadRepository, AnswerReadRepository>();
            services.AddScoped<IAnswerWriteRepository, AnswerWriteRepository>();

            services.AddScoped<ICommentReadRepository, CommentReadRepository>();
            services.AddScoped<ICommentReadRepository, CommentReadRepository>();


            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        }
    }
}