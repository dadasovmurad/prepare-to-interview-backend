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
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        //public DbSet<QuestionTranslation> QuestionTranslations { get; set; }
        //public DbSet<CategoryTranslation> CategoryTranslations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Translations
            //modelBuilder.Entity<QuestionTranslation>(entity =>
            //{
            //    entity.HasOne(q => q.Question)
            //        .WithMany(q => q.QuestionTranslations)
            //        .HasForeignKey(q => q.QuestionId);
            //});

            //modelBuilder.Entity<CategoryTranslation>(entity =>
            //{
            //    entity.HasOne(q => q.Category)
            //        .WithMany(q => q.CategoryTranslations)
            //        .HasForeignKey(q => q.CategoryId);
            //});
            #endregion

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Difficulty)
                    .HasConversion<string>();

                entity.HasOne(q => q.Category)
                    .WithMany(c => c.Questions)
                    .HasForeignKey(q => q.CategoryId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(s => s.Parent)
                    .WithMany(m => m.Children)
                    .HasForeignKey(e => e.ParentId);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasOne(c => c.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(c => c.QuestionId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(c => c.Question)
                    .WithMany(q => q.Comments)
                    .HasForeignKey(c => c.QuestionId);
            });

            modelBuilder.Entity<QuestionTag>(entity =>
            {
                entity.HasKey(qt => new { qt.QuestionID, qt.TagID });

                entity.HasOne(qt => qt.Question)
                    .WithMany(q => q.QuestionTags)
                    .HasForeignKey(qt => qt.QuestionID);

                entity.HasOne(qt => qt.Tag)
                    .WithMany(t => t.QuestionTags)
                    .HasForeignKey(qt => qt.TagID);
            });

            modelBuilder.Entity<Contribution>(entity =>
            {
                entity.Property(c => c.Tags)
                    .HasColumnType("jsonb");

                entity.Property(e => e.Difficulty)
                    .HasConversion<string>();

                entity.HasOne(c => c.User)
                    .WithMany(q => q.Contributions)
                    .HasForeignKey(c => c.UserId);
            });
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasMany(u => u.Contributions)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId);

                entity.HasIndex(u => u.PassKeyHash)
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}