CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "Users"
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

CREATE TABLE "Wallets"
(
    "wallet_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "numberwallet" float,
    "user_id"      UUID,
    "created_at"   TIMESTAMP,
    "updated_at"   TIMESTAMP,
    "isDeleted"    BOOLEAN
);

CREATE TABLE "Achievements"
(
    "achievement_id"      UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "achievement_name"    VARCHAR,
    "achievement_content" VARCHAR,
    "user_id"             UUID,
    "created_at"          TIMESTAMP,
    "isDeleted"           BOOLEAN
);

CREATE TABLE "Roles"
(
    "role_id"    UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "role_name"  VARCHAR,
    "isDeleted"  BOOLEAN,
    "created_at" TIMESTAMP,
    "updated_at" TIMESTAMP
);

CREATE TABLE "Subscriptions"
(
    "subscription_id"   UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "subscription_name" VARCHAR,
    "subscription_body" VARCHAR,
    "price"             FLOAT,
    "duration" INTERVAL,
    "created_at"        TIMESTAMP,
    "isDeleted"         BOOLEAN
);

CREATE TABLE "Transactions"
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

CREATE TABLE "Subjects"
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

CREATE TABLE "Chapters"
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

CREATE TABLE "Lessons"
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

CREATE TABLE "Notes"
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

CREATE TABLE "Reports"
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

CREATE TABLE "Theories"
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

CREATE TABLE "Quizzes"
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

CREATE TABLE "Discussions"
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

CREATE TABLE "Answers"
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

CREATE TABLE "Flashcards"
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

CREATE TABLE "Flashcard_contents"
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

CREATE TABLE "Calculations"
(
    "subject_id"    UUID,
    "lesson_count"  INT,
    "chapter_count" INT,
    "quiz_count"    INT,
    "theory_count"  INT,
    "created_at"    TIMESTAMP,
    "updated_at"    TIMESTAMP
);

CREATE TABLE "Notifications"
(
    "notification_id"      UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "notification_name"    VARCHAR,
    "notification_message" VARCHAR,
    "user_id"              UUID,
    "read_at"              TIMESTAMP
);

CREATE TABLE "Enrollments"
(
    "user_id"    UUID,
    "subject_id" UUID,
    "process"    FLOAT,
    "updated_at" TIMESTAMP,
    "created_at" TIMESTAMP,
    PRIMARY KEY ("user_id", "subject_id")
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
ALTER TABLE "Users"
    ADD FOREIGN KEY ("role_id") REFERENCES "Roles" ("role_id");

ALTER TABLE "theory_flashcard_content"
    ADD FOREIGN KEY ("theory_id") REFERENCES "Theories" ("theory_id");

ALTER TABLE "Notifications"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Answers"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

--need consider
ALTER TABLE "Wallets"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Enrollments"
    ADD FOREIGN KEY ("subject_id") REFERENCES "Subjects" ("subject_id");

ALTER TABLE "Enrollments"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Achievements"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Reports"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Wallets"
    ADD FOREIGN KEY ("transaction_id") REFERENCES "Transactions" ("transaction_id");

ALTER TABLE "Flashcard_contents"
    ADD FOREIGN KEY ("flashcard_id") REFERENCES "Flashcards" ("flashcard_id");

ALTER TABLE "Flashcards"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Transactions"
    ADD FOREIGN KEY ("subscription_id") REFERENCES "Subscriptions" ("subscription_id");

ALTER TABLE "Discussions"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Discussions"
    ADD FOREIGN KEY ("subject_id") REFERENCES "Subjects" ("subject_id");

ALTER TABLE "Answers"
    ADD FOREIGN KEY ("question_id") REFERENCES "Discussions" ("discussion_id");

ALTER TABLE "Notes"
    ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("user_id");

ALTER TABLE "Flashcards"
    ADD FOREIGN KEY ("subject_id") REFERENCES "Subjects" ("subject_id");

ALTER TABLE "Lessons"
    ADD FOREIGN KEY ("chapter_id") REFERENCES "Chapters" ("chapter_id");

ALTER TABLE "Quizzes"
    ADD FOREIGN KEY ("lesson_id") REFERENCES "Lessons" ("lesson_id");

ALTER TABLE "Theories"
    ADD FOREIGN KEY ("lesson_id") REFERENCES "Lessons" ("lesson_id");

ALTER TABLE "Chapters"
    ADD FOREIGN KEY ("subject_id") REFERENCES "Subjects" ("subject_id");