CREATE TABLE IF NOT EXISTS "Languages" (
    "Id" SERIAL PRIMARY KEY,
    "Code" VARCHAR(10) NOT NULL UNIQUE,
    "Name" VARCHAR(100) NOT NULL
);

INSERT INTO "Languages" ("Code", "Name") VALUES
     ('pt', 'Português'),
     ('en', 'English'),
     ('es', 'Español'),
     ('it', 'Italiano'),
     ('de', 'Deutsch'),
     ('fr', 'French')
ON CONFLICT ("Code") DO NOTHING;