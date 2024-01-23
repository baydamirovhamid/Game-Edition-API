CREATE TABLE "game" (
  "id" integer PRIMARY KEY,
  "title" varchar,
  "releasedate" timestamp,
  "genre" varchar,
  "price" integer,
  "company_id" integer,
  "platform_id" integer,
  "created_at" timestamp,
  "created_by" varchar,
  "updated_at" timestamp,
  "updated_by" varchar
);

CREATE TABLE "company" (
  "id" integer PRIMARY KEY,
  "name" varchar,
  "email" varchar,
  "address" varchar,
  "created_at" datetime,
  "created_by" varchar,
  "updated_at" datetime,
  "updated_by" varchar,
);

CREATE TABLE "platform" (
  "id" integer PRIMARY KEY,
  "name" varchar,
  "releasedate" date,
  created_at datetime,
  created_by varchar,
  updated_at datetime,
  updated_by varchar
);

CREATE TABLE "game_company" (
  "id" integer,
  "company_id" integer,
  "game_id" integer
);

CREATE TABLE "game_platform" (
  "id" integer,
  "platform_id" integer,
  "game_id" integer
);

CREATE TABLE "static_data" (
  "id" integer PRIMARY KEY,
  "name" varchar,
  "key" varchar
);

CREATE TABLE "basket" (
  "id" integer,
  "customer_id" integer,
  "game_id" integer,
  "date" datetime,
  "total_amount" integer,
  "created_at" datetime,
  "created_by" varchar,
  "updated_at" datetime,
  "updated_by" varchar,
  "is_deleted" integer
);

CREATE TABLE "customer" (
  "id" integer,
  "name" varchar,
  "surname" varchar,
  "phone" varchar,
  "email" varchar,
  "user_id" integer,
  "birthdate" datetime,
  "created_at" datetime,
  "created_by" varchar,
  "updated_at" datetime,
  "updated_by" varchar
);

ALTER TABLE "game" ADD FOREIGN KEY ("company_id") REFERENCES "company" ("id");

ALTER TABLE "game_platform" ADD FOREIGN KEY ("platform_id") REFERENCES "platform" ("id");

ALTER TABLE "game_company" ADD FOREIGN KEY ("company_id") REFERENCES "company" ("id");

ALTER TABLE "game_company" ADD FOREIGN KEY ("game_id") REFERENCES "game" ("id");

ALTER TABLE "game_platform" ADD FOREIGN KEY ("game_id") REFERENCES "game" ("id");

ALTER TABLE "basket" ADD FOREIGN KEY ("customer_id") REFERENCES "customer" ("id");

ALTER TABLE "basket" ADD FOREIGN KEY ("game_id") REFERENCES "game" ("id");

ALTER TABLE "customer" ADD FOREIGN KEY ("user_id") REFERENCES "user" ("Id");