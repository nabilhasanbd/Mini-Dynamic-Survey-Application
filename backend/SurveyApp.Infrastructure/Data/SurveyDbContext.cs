using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.Entities;
using SurveyApp.Domain.Enums;
using SurveyApp.Infrastructure.Data.Configurations;

namespace SurveyApp.Infrastructure.Data
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure enum to string conversion
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionType)
                .HasConversion<string>();

            // Apply configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SurveyConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionConfiguration());
            modelBuilder.ApplyConfiguration(new ResponseConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());

            // Indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Survey>()
                .HasIndex(s => s.Slug)
                .IsUnique();

            modelBuilder.Entity<Survey>()
                .HasIndex(s => s.IsPublished);

            modelBuilder.Entity<Survey>()
                .HasIndex(s => s.IsDeleted);

            modelBuilder.Entity<Survey>()
                .HasIndex(s => s.UserId);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.SurveyId);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.QuestionType);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.OrderIndex);

            modelBuilder.Entity<QuestionOption>()
                .HasIndex(qo => qo.QuestionId);

            modelBuilder.Entity<Response>()
                .HasIndex(r => r.SurveyId);

            modelBuilder.Entity<Response>()
                .HasIndex(r => r.SubmittedAt);

            modelBuilder.Entity<Answer>()
                .HasIndex(a => a.ResponseId);

            modelBuilder.Entity<Answer>()
                .HasIndex(a => a.QuestionId);

            // Cascade delete rules
            modelBuilder.Entity<Survey>()
                .HasOne(s => s.User)
                .WithMany(u => u.Surveys)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionOption>()
                .HasOne(qo => qo.Question)
                .WithMany(q => q.QuestionOptions)
                .HasForeignKey(qo => qo.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Response>()
                .HasOne(r => r.Survey)
                .WithMany(s => s.Responses)
                .HasForeignKey(r => r.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Response)
                .WithMany(r => r.Answers)
                .HasForeignKey(a => a.ResponseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Value conversions for JSONB
            modelBuilder.Entity<Answer>()
                .Property(a => a.SelectedOptionIds)
                .HasColumnType("jsonb");
        }
    }
}