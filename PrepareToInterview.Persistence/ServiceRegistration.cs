using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrepareToInterview.Persistence.Contexts;
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
        }
    }
}