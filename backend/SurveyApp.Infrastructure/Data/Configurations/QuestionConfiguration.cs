using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(q => q.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(q => q.UpdatedAt).HasDefaultValueSql("NOW()");
        }
    }
}