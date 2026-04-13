using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(u => u.UpdatedAt).HasDefaultValueSql("NOW()");
        }
    }
}