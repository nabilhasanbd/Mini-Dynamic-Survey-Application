-- Seed default admin user for M&E System
-- Note: Password hash needs to be generated through ASP.NET Core Identity
-- This script creates the user structure but you'll need to either:
-- 1. Generate the proper password hash using .NET, or
-- 2. Use the API registration endpoint to create the admin user

-- Admin user data (Password: Admin@123)
-- The password hash below is a placeholder - replace with actual hash
-- or use the API to register the admin user

-- Insert admin user
INSERT INTO "Users" (
    "Id", 
    "FirstName", 
    "LastName", 
    "Phone", 
    "Designation", 
    "Organization", 
    "IsActive", 
    "CreatedAt", 
    "UserName", 
    "NormalizedUserName", 
    "Email", 
    "NormalizedEmail", 
    "EmailConfirmed", 
    "PasswordHash",
    "SecurityStamp",
    "LockoutEnabled",
    "AccessFailedCount"
)
VALUES 
    (
        'admin-user-id-12345',
        'System',
        'Administrator',
        '+1234567890',
        'System Administrator',
        'M&E System',
        true,
        CURRENT_TIMESTAMP,
        'admin@mne.local',
        'ADMIN@MNE.LOCAL',
        'admin@mne.local',
        'ADMIN@MNE.LOCAL',
        true,
        NULL, -- Password hash - needs to be generated or use API
        'admin-security-stamp',
        true,
        0
    )
ON CONFLICT ("Id") DO NOTHING;

-- Assign Admin role to admin user
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES 
    ('admin-user-id-12345', 'admin-role-id')
ON CONFLICT DO NOTHING;

-- Admin user structure created
-- Password hash must be set through API or proper .NET password hashing
COMMIT;