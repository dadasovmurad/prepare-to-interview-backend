using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Persistence.Contexts
{
    public class PrepareToInterviewAPIDbContext : DbContext
    {
        public PrepareToInterviewAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Question> Answers { get; set; }
        public DbSet<Question> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Question>()
           //.HasOne(q => q.User)
           //.WithMany(u => u.Questions)
           //.HasForeignKey(q => q.UserId);

            //modelBuilder.Entity<Question>()
            //    .HasMany(q => q.Answer)
            //    .WithOne(a => a.Question)
            //    .HasForeignKey(a => a.QuestionId);

            modelBuilder.Entity<Answer>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(c => c.QuestionId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Comments)
                .HasForeignKey(c => c.QuestionId);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.User)
            //    .WithMany()
            //    .HasForeignKey(c => c.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
