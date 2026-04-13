using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Data.Configurations
{
    public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.Property(qo => qo.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(qo => qo.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(qo => qo.UpdatedAt).HasDefaultValueSql("NOW()");
        }
    }
}