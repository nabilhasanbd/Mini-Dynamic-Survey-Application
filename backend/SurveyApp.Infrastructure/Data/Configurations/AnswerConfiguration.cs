using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Data.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(a => a.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(a => a.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(a => a.UpdatedAt).HasDefaultValueSql("NOW()");
        }
    }
}