using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

namespace MneSystem.Infrastructure.Data.Configurations;

public class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.ToTable("UserLogins");

        builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });

        builder.Property(x => x.LoginProvider)
            .HasMaxLength(128);

        builder.Property(x => x.ProviderKey)
            .HasMaxLength(128);

        builder.Property(x => x.ProviderDisplayName)
            .HasMaxLength(256);
    }
}