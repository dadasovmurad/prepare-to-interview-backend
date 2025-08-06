using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PrepareToInterview.Persistence.Contexts
{
    public class PrepareToInterviewAPIDbContextFactory : IDesignTimeDbContextFactory<PrepareToInterviewAPIDbContext>
    {
        public PrepareToInterviewAPIDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<PrepareToInterviewAPIDbContext>();
            var connectionString = configuration.GetConnectionString("PostgreSQL")
                                   ?? configuration["ConnectionStrings:PostgreSQL"];

            builder.UseNpgsql(connectionString);

            return new PrepareToInterviewAPIDbContext(builder.Options);
        }
    }
}