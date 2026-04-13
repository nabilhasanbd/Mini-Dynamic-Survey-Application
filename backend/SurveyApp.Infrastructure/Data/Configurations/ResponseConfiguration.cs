using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Data.Configurations
{
    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.Property(r => r.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(r => r.SubmittedAt).HasDefaultValueSql("NOW()");
            builder.Property(r => r.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(r => r.UpdatedAt).HasDefaultValueSql("NOW()");
        }
    }
}