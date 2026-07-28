using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MneSystem.Domain.Entities;
using MneSystem.Domain.Enums;

namespace MneSystem.Infrastructure.Data.Seeding;

public static class ApplicationDbContextSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { AppRoles.Admin, AppRoles.MeOfficer, AppRoles.FieldOfficer };

        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public static async Task SeedDefaultAdminAsync(UserManager<ApplicationUser> userManager)
    {
        var adminUser = new ApplicationUser
        {
            UserName = "admin@mne.local",
            Email = "admin@mne.local",
            EmailConfirmed = true,
            FirstName = "System",
            LastName = "Administrator",
            Phone = "+1234567890",
            Designation = "System Administrator",
            Organization = "M&E System",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var existingAdmin = await userManager.FindByEmailAsync(adminUser.Email);
        if (existingAdmin == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
            }
        }
    }

    public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRolesAsync(roleManager);
            await SeedDefaultAdminAsync(userManager);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding database: {ex.Message}");
            throw;
        }
    }
}