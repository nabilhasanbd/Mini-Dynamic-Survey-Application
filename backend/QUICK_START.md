# M&E System - Quick Start Guide

## Database Setup

Since .NET SDK is not available, use the SQL scripts to set up your PostgreSQL database:

### Quick Setup (Recommended)

Run the single comprehensive script:

```bash
psql -U postgres -d mne_system -f sql/00_QuickSetup.sql
```

This will create all tables and seed the required roles.

### Step-by-Step Setup

1. **Create database** (if needed):
```bash
createdb -U postgres mne_system
```

2. **Create tables and seed roles**:
```bash
psql -U postgres -d mne_system -f sql/00_QuickSetup.sql
```

## Connection String

Update `src/MneSystem.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mne_system;Username=postgres;Password=your_password"
  }
}
```

## Create Admin User

Use the API registration endpoint to create the admin user:

```bash
POST http://localhost:5000/api/auth/register
Content-Type: application/json

{
  "email": "admin@mne.local",
  "password": "Admin@123",
  "firstName": "System",
  "lastName": "Administrator",
  "phone": "+1234567890",
  "designation": "System Administrator",
  "organization": "M&E System"
}
```

Then manually assign the Admin role:

```sql
-- Get the user ID first
SELECT "Id" FROM "Users" WHERE "Email" = 'admin@mne.local';

-- Update role assignment
UPDATE "UserRoles" 
SET "RoleId" = 'admin-role-id' 
WHERE "UserId" = 'your-user-id-here';

-- Or insert if needed
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES ('your-user-id-here', 'admin-role-id');
```

## Verify Setup

```sql
-- Check tables
SELECT table_name FROM information_schema.tables 
WHERE table_schema = 'public';

-- Check roles
SELECT * FROM "Roles";

-- Check users
SELECT "Id", "Email", "FirstName", "LastName" FROM "Users";
```

## Test the API

1. Register a new user
2. Login to verify authentication
3. Try user management endpoints
4. Verify database updates

## Troubleshooting

### Connection Issues
```bash
# Test PostgreSQL connection
psql -U postgres -d mne_system -c "SELECT version();"
```

### Clean Start
```sql
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO postgres;
```

Then re-run the setup script.

## Available SQL Scripts

- `00_QuickSetup.sql` - Complete setup (tables + roles)
- `01_CreateIdentityTables.sql` - Create Identity tables only
- `02_SeedRoles.sql` - Seed roles only
- `03_SeedAdminUser.sql` - Create admin user structure (requires password hash)

See `README_DATABASE_SETUP.md` for detailed instructions.