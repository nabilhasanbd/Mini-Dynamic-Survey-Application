using Microsoft.AspNetCore.Identity;
using MneSystem.Domain.Entities;

namespace MneSystem.Infrastructure.Data;

public class MneDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public MneDbContext(DbContextOptions<MneDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MneDbContext).Assembly);
    }
}