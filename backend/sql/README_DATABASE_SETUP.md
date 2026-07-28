# Database Setup for M&E System

Since the .NET SDK is not available in your environment, use these SQL scripts to set up the PostgreSQL database manually.

## Prerequisites

1. PostgreSQL database server running
2. Database created (e.g., `mne_system`)
3. Access to execute SQL scripts against the database

## Setup Instructions

### Step 1: Create the Database (if not exists)

```sql
CREATE DATABASE mne_system;
```

### Step 2: Create Identity Tables

Execute `sql/01_CreateIdentityTables.sql` against your database:

```bash
psql -U postgres -d mne_system -f sql/01_CreateIdentityTables.sql
```

This will create:
- Users table (extended with M&E specific fields)
- Roles table
- UserRoles table
- RoleClaims, UserClaims tables
- UserLogins, UserTokens tables
- All necessary indexes and constraints

### Step 3: Seed Roles

Execute `sql/02_SeedRoles.sql`:

```bash
psql -U postgres -d mne_system -f sql/02_SeedRoles.sql
```

This will create:
- Admin role
- M&E Officer role
- Field Officer role

### Step 4: Create Default Admin User

**Option A: Use API Registration (Recommended)**

Once your API is running, register the admin user via the API:

```bash
POST /api/auth/register
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

Then manually assign the Admin role via database:

```sql
-- Update user role to Admin
UPDATE "UserRoles" 
SET "RoleId" = 'admin-role-id' 
WHERE "UserId" = 'your-registered-user-id';

-- Or insert if role assignment doesn't exist
INSERT INTO "UserRoles" ("UserId", "RoleId")
VALUES ('your-registered-user-id', 'admin-role-id');
```

**Option B: Manual SQL Creation**

Execute `sql/03_SeedAdminUser.sql` (requires proper password hash):

```bash
psql -U postgres -d mne_system -f sql/03_SeedAdminUser.sql
```

Note: You'll need to generate the proper password hash using ASP.NET Core Identity or update it through the API.

## Connection String

Update your `src/MneSystem.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mne_system;Username=postgres;Password=your_password"
  }
}
```

## Verify Setup

After running the scripts, verify the setup:

```sql
-- Check tables exist
SELECT table_name FROM information_schema.tables 
WHERE table_schema = 'public' AND table_name LIKE '%User%' OR table_name LIKE '%Role%';

-- Check roles are seeded
SELECT * FROM "Roles";

-- Check admin user (if created)
SELECT "Id", "Email", "FirstName", "LastName", "IsActive" FROM "Users";
```

## Troubleshooting

### Connection Issues

Ensure PostgreSQL is running and credentials are correct:

```bash
# Test connection
psql -U postgres -d mne_system -c "SELECT version();"
```

### Permission Issues

Grant necessary permissions:

```sql
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO postgres;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO postgres;
```

### Clean Start

If you need to start fresh:

```sql
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO postgres;
```

Then re-run the SQL scripts.

## Next Steps

1. Once the database is set up, your API should be able to connect and function
2. Test the authentication endpoints
3. Verify user registration and login functionality
4. Create additional users and assign roles as needed