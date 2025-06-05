using Microsoft.EntityFrameworkCore;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Persistence.Contexts
{
    public class PrepareToInterviewAPIDbContext : DbContext
    {
        public PrepareToInterviewAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        //public DbSet<QuestionTranslation> QuestionTranslations { get; set; }
        //public DbSet<CategoryTranslation> CategoryTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.CategoryId);

            //modelBuilder.Entity<QuestionTranslation>()
            //    .HasOne(q => q.Question)
            //    .WithMany(q => q.QuestionTranslations)
            //    .HasForeignKey(q => q.QuestionId);

            modelBuilder.Entity<Category>()
                .HasOne(s => s.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(e => e.ParentId);

            //modelBuilder.Entity<CategoryTranslation>()
            //    .HasOne(q => q.Category)
            //    .WithMany(q => q.CategoryTranslations)
            //    .HasForeignKey(q => q.CategoryId);

            modelBuilder.Entity<Answer>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(c => c.QuestionId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Question)
                .WithMany(q => q.Comments)
                .HasForeignKey(c => c.QuestionId);

            modelBuilder.Entity<QuestionTag>()
                .HasKey(qt => new { qt.QuestionID, qt.TagID });

            modelBuilder.Entity<QuestionTag>()
                .HasOne(qt => qt.Question)
                .WithMany(q => q.QuestionTags)
                .HasForeignKey(qt => qt.QuestionID);

            modelBuilder.Entity<QuestionTag>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestionTags)
                .HasForeignKey(qt => qt.TagID);

            modelBuilder.Entity<Contribution>()
                .Property(c => c.Tags)
                .HasColumnType("jsonb");

            modelBuilder.Entity<Question>()
           .Property(e => e.Difficulty)
           .HasConversion<string>();  // Store enum as string in the database
           
            modelBuilder.Entity<Contribution>()
           .Property(e => e.Difficulty)
           .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
