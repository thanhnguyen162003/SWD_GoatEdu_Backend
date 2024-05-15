CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "users"
(
    "user_id"          UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "username"         VARCHAR,
    "password"         VARCHAR,
    "email"            VARCHAR,
    "phone_number"     VARCHAR,
    "subscription"     VARCHAR,
    "subscription_end" TIMESTAMP,
    "role_id"          UUID,
    "wallet_id"        UUID,
    "created_at"       TIMESTAMP,
    "updated_at"       TIMESTAMP,
    "isDeleted"        BOOLEAN
);

CREATE TABLE "wallets"
(
    "wallet_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "numberwallet" float,
    "user_id"      UUID,
    "created_at"   TIMESTAMP,
    "updated_at"   TIMESTAMP,
    "isDeleted"    BOOLEAN
);

CREATE TABLE "achievements"
(
    "achievement_id"      UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "achievement_name"    VARCHAR,
    "achievement_content" VARCHAR,
    "user_id"             UUID,
    "created_at"          TIMESTAMP,
    "isDeleted"           BOOLEAN
);

CREATE TABLE "moderators"
(
    "moderator_id"       UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "moderator_name"     VARCHAR,
    "password"           VARCHAR,
    "email"              VARCHAR,
    "phone_number"       VARCHAR,
    "is_password_change" BOOLEAN,
    "created_by"         VARCHAR,
    "created_at"         TIMESTAMP,
    "isDeleted"          BOOLEAN
);

CREATE TABLE "roles"
(
    "role_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "role_name"  VARCHAR,
    "isDeleted"  BOOLEAN,
    "created_at" TIMESTAMP,
    "updated_at" TIMESTAMP
);

CREATE TABLE "subscriptions"
(
    "subscription_id"   UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "subscription_name" VARCHAR,
    "subscription_body" VARCHAR,
    "price"             FLOAT,
    "duration" INTERVAL,
    "created_at"        TIMESTAMP,
    "isDeleted"         BOOLEAN
);

CREATE TABLE "transactions"
(
    "transaction_id"   UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "transaction_name" VARCHAR,
    "note"             VARCHAR,
    -- need walletId
    "wallet_id"        UUID,
    "start_date"       TIMESTAMP,
    "end_date"         TIMESTAMP,
    "subscription_id"  UUID,
    "created_at"       TIMESTAMP,
    "isDeleted"        BOOLEAN
);

CREATE TABLE "subjects"
(
    "subject_id"   UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "subject_name" VARCHAR,
    "subject_code" VARCHAR,
    "information"  VARCHAR,
    "class"        VARCHAR,
    "image"        VARCHAR,
    "isDeleted"    BOOLEAN,
    "created_at"   TIMESTAMP,
    "updated_at"   TIMESTAMP
);

CREATE TABLE "chapters"
(
    "chapter_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "chapter_name"  VARCHAR,
    "chapter_level" INT,
    "subject_id"    UUID,
    "isDeleted"     BOOLEAN,
    "created_at"    TIMESTAMP,
    "created_by"    VARCHAR,
    "updated_at"    TIMESTAMP
);

CREATE TABLE "lessons"
(
    "lesson_id"       UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "lesson_name"     VARCHAR,
    "lesson_body"     VARCHAR,
    "lesson_material" VARCHAR,
    "chapter_id"      UUID,
    "created_at"      TIMESTAMP,
    "created_by"      VARCHAR,
    "updated_by"      VARCHAR,
    "updated_at"      TIMESTAMP,
    "isDeleted"       BOOLEAN
);

CREATE TABLE "notes"
(
    "note_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "note_name"  VARCHAR,
    "note_body"  VARCHAR,
    "user_id"    UUID,
    "created_at" TIMESTAMP,
    "created_by" VARCHAR,
    "updated_by" VARCHAR,
    "updated_at" TIMESTAMP,
    "isDeleted"  BOOLEAN
);

CREATE TABLE "reports"
(
    "report_id"      UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "report_title"   VARCHAR,
    "report_content" VARCHAR,
    "user_id"        UUID,
    "created_at"     TIMESTAMP,
    "created_by"     VARCHAR,
    "status"         VARCHAR,
    "isDeleted"      BOOLEAN
);

CREATE TABLE "theories"
(
    "theory_id"      UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "theory_name"    VARCHAR,
    "file"           VARCHAR,
    "image"          VARCHAR,
    "theory_content" VARCHAR,
    "lesson_id"      UUID,
    "created_at"     TIMESTAMP,
    "updated_at"     TIMESTAMP,
    "isDeleted"      BOOLEAN
);

CREATE TABLE "quizzes"
(
    "quiz_id"        UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "quiz"           VARCHAR,
    "answer_correct" VARCHAR,
    "answer_1"       VARCHAR,
    "answer_2"       VARCHAR,
    "answer_3"       VARCHAR,
    "lesson_id"      UUID,
    "created_at"     TIMESTAMP,
    "updated_at"     TIMESTAMP,
    "isDeleted"      BOOLEAN
);

CREATE TABLE "discussions"
(
    "discussion_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "discussion_name"  VARCHAR,
    "discussion_body"  VARCHAR,
    "user_id"          UUID,
    "discussion_image" VARCHAR,
    "discussion_vote"  INT,
    "tag"              VARCHAR,
    "subject_id"       UUID,
    "is_solved"        BOOLEAN,
    "status"           VARCHAR,
    "created_at"       TIMESTAMP,
    "created_by"       VARCHAR,
    "updated_by"       VARCHAR,
    "updated_at"       TIMESTAMP,
    "isDeleted"        BOOLEAN
);

CREATE TABLE "answers"
(
    "answer_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "answer_name"  VARCHAR,
    "answer_body"  VARCHAR,
    "user_id"      UUID,
    "question_id"  UUID,
    "answer_image" VARCHAR,
    "answer_vote"  INT,
    "tag"          VARCHAR,
    "created_at"   TIMESTAMP,
    "created_by"   VARCHAR,
    "updated_by"   VARCHAR,
    "updated_at"   TIMESTAMP,
    "isDeleted"    BOOLEAN
);

CREATE TABLE "flashcards"
(
    "flashcard_id"          UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "flashcard_name"        VARCHAR,
    "flashcard_description" VARCHAR,
    "user_id"               UUID,
    "tag"                   VARCHAR,
    "subject_id"            UUID,
    "status"                VARCHAR,
    "star"                  INT,
    "created_at"            TIMESTAMP,
    "created_by"            VARCHAR,
    "updated_by"            VARCHAR,
    "updated_at"            TIMESTAMP,
    "isDeleted"             BOOLEAN
);

CREATE TABLE "flashcard_contents"
(
    "flashcard_content_id"       UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "flashcard_content_question" VARCHAR,
    "flashcard_content_answer"   VARCHAR,
    "flashcard_id"               UUID,
    "image"                      VARCHAR,
    "status"                     VARCHAR,
    "created_at"                 TIMESTAMP,
    "created_by"                 VARCHAR,
    "updated_by"                 VARCHAR,
    "updated_at"                 TIMESTAMP,
    "isDeleted"                  BOOLEAN
);

CREATE TABLE "calculations"
(
    "subject_id"    UUID,
    "lesson_count"  INT,
    "chapter_count" INT,
    "quiz_count"    INT,
    "theory_count"  INT,
    "created_at"    TIMESTAMP,
    "updated_at"    TIMESTAMP
);

CREATE TABLE "notifications"
(
    "notification_id"      UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "notification_name"    VARCHAR,
    "notification_message" VARCHAR,
    "user_id"              UUID,
    "read_at"              TIMESTAMP
);

CREATE TABLE "user_subject"
(
    "user_id"    UUID,
    "subject_id" UUID,
    "process"    FLOAT,
    "updated_at" TIMESTAMP,
    "created_at" TIMESTAMP,
    PRIMARY KEY ("user_id", "subject_id")
);

CREATE TABLE "admins"
(
    "admin_id"  UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "username"  VARCHAR,
    "password"  VARCHAR,
    "isDeleted" BOOLEAN
);

CREATE TABLE "theory_flashcard_content"
(
    "id"         UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "question"   VARCHAR,
    "answer"     VARCHAR,
    "theory_id"  UUID,
    "status"     VARCHAR,
    "created_at" TIMESTAMP,
    "updated_at" TIMESTAMP,
    "isDeleted"  BOOLEAN
);

-- Add foreign key constraints
ALTER TABLE "users"
    ADD FOREIGN KEY ("role_id") REFERENCES "roles" ("role_id");

ALTER TABLE "theory_flashcard_content"
    ADD FOREIGN KEY ("theory_id") REFERENCES "theories" ("theory_id");

ALTER TABLE "notifications"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "answers"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

--need consider
ALTER TABLE "wallets"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "user_subject"
    ADD FOREIGN KEY ("subject_id") REFERENCES "subjects" ("subject_id");

ALTER TABLE "user_subject"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "achievements"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "reports"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "wallets"
    ADD FOREIGN KEY ("transaction_id") REFERENCES "transactions" ("transaction_id");

ALTER TABLE "flashcard_contents"
    ADD FOREIGN KEY ("flashcard_id") REFERENCES "flashcards" ("flashcard_id");

ALTER TABLE "flashcards"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "transactions"
    ADD FOREIGN KEY ("subscription_id") REFERENCES "subscriptions" ("subscription_id");

ALTER TABLE "discussions"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "discussions"
    ADD FOREIGN KEY ("subject_id") REFERENCES "subjects" ("subject_id");

ALTER TABLE "answers"
    ADD FOREIGN KEY ("question_id") REFERENCES "discussions" ("discussion_id");

ALTER TABLE "notes"
    ADD FOREIGN KEY ("user_id") REFERENCES "users" ("user_id");

ALTER TABLE "flashcards"
    ADD FOREIGN KEY ("subject_id") REFERENCES "subjects" ("subject_id");

ALTER TABLE "lessons"
    ADD FOREIGN KEY ("chapter_id") REFERENCES "chapters" ("chapter_id");

ALTER TABLE "quizzes"
    ADD FOREIGN KEY ("lesson_id") REFERENCES "lessons" ("lesson_id");

ALTER TABLE "theories"
    ADD FOREIGN KEY ("lesson_id") REFERENCES "lessons" ("lesson_id");

ALTER TABLE "chapters"
    ADD FOREIGN KEY ("subject_id") REFERENCES "subjects" ("subject_id");