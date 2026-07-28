-- Seed roles for M&E System
-- Run this after creating the tables

-- Insert roles
INSERT INTO "Roles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES 
    ('admin-role-id', 'Admin', 'ADMIN', 'admin-stamp')
ON CONFLICT ("Id") DO NOTHING;

INSERT INTO "Roles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES 
    ('me-officer-role-id', 'M&E Officer', 'M&E OFFICER', 'me-officer-stamp')
ON CONFLICT ("Id") DO NOTHING;

INSERT INTO "Roles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES 
    ('field-officer-role-id', 'Field Officer', 'FIELD OFFICER', 'field-officer-stamp')
ON CONFLICT ("Id") DO NOTHING;

-- Roles seeded successfully
COMMIT;