using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Data.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.Property(s => s.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(s => s.UpdatedAt).HasDefaultValueSql("NOW()");
        }
    }
}