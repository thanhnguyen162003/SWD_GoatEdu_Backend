CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "User"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "username" VARCHAR,
    "password" VARCHAR,
    "email" VARCHAR,
    "phoneNumber" VARCHAR,
    "subscription" VARCHAR,
    "subscriptionEnd" TIMESTAMP,
    "provider" VARCHAR,
    "emailVerify" BOOLEAN,
    "image" VARCHAR,
    "fullname" VARCHAR,
    "roleId" UUID,
    "walletId" UUID,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Wallet"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "numberWallet" FLOAT,
    --"userId" UUID,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Achievement"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "achievementName" VARCHAR,
    "achievementContent" VARCHAR,
    "userId" UUID,
    "createdAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Role"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "roleName" VARCHAR,
    "isDeleted" BOOLEAN,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP
);

CREATE TABLE "Subscription"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "subscriptionName" VARCHAR,
    "subscriptionBody" VARCHAR,
    "price" FLOAT,
    "duration" INTERVAL,
    "createdAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Transaction"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "transactionName" VARCHAR,
    "note" VARCHAR,
    "walletId" UUID,
    "startDate" TIMESTAMP,
    "endDate" TIMESTAMP,
    "subscriptionId" UUID,
    "createdAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Subject"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "subjectName" VARCHAR,1
    "subjectCode" VARCHAR,
    "information" VARCHAR,
    "class" VARCHAR,
    "image" VARCHAR,
    "isDeleted" BOOLEAN,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP
);

CREATE TABLE "Chapter"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "chapterName" VARCHAR,
    "chapterLevel" INT,
    "subjectId" UUID,
    "isDeleted" BOOLEAN,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedAt" TIMESTAMP
);

CREATE TABLE "Lesson"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "lessonName" VARCHAR,
    "lessonBody" VARCHAR,
    "lessonMaterial" VARCHAR,
    "chapterId" UUID,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedBy" VARCHAR,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN,
    "displayOrder" INT
);

CREATE TABLE "Note"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "noteName" VARCHAR,
    "noteBody" VARCHAR,
    "userId" UUID,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedBy" VARCHAR,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Report"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "reportTitle" VARCHAR,
    "reportContent" VARCHAR,
    "userId" UUID,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "status" VARCHAR,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Theory"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "theoryName" VARCHAR,
    "file" VARCHAR,
    "image" VARCHAR,
    "theoryContent" VARCHAR,
    "lessonId" UUID,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Quiz"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "quiz" VARCHAR,
    "quizLevel" INT,
    "lessonId" UUID,
    "chapterId" UUID,
    "subjectId" UUID,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN,
    "isRequire" BOOLEAN
);

CREATE TABLE "QuestionInQuiz"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "quizId" UUID,
    "quizQuestion" VARCHAR,
    "quizAnswer1" VARCHAR,
    "quizAnswer2" VARCHAR,
    "quizAnswer3" VARCHAR,
    "quizCorrect" VARCHAR,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN,
    FOREIGN KEY ("quizId") REFERENCES "Quiz" ("id")
);

CREATE TABLE "Discussion"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "discussionName" VARCHAR,
    "discussionBody" VARCHAR,
    "userId" UUID,
    "discussionImage" VARCHAR,
    "discussionVote" INT,
    "tag" VARCHAR,
    "subjectId" UUID,
    "isSolved" BOOLEAN,
    "status" VARCHAR,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedBy" VARCHAR,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Answer"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "answerName" VARCHAR,
    "answerBody" VARCHAR,
    "userId" UUID,
    "questionId" UUID,
    "answerImage" VARCHAR,
    "answerVote" INT,
    "tag" VARCHAR,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedBy" VARCHAR,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Flashcard"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "flashcardName" VARCHAR,
    "flashcardDescription" VARCHAR,
    "userId" UUID,
    "tag" VARCHAR,
    "subjectId" UUID,
    "status" VARCHAR,
    "star" INT,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedBy" VARCHAR,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "FlashcardContent"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "flashcardContentQuestion" VARCHAR,
    "flashcardContentAnswer" VARCHAR,
    "flashcardId" UUID,
    "image" VARCHAR,
    "status" VARCHAR,
    "createdAt" TIMESTAMP,
    "createdBy" VARCHAR,
    "updatedBy" VARCHAR,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN
);

CREATE TABLE "Calculation"
(
    "id" UUID,
    "lessonCount" INT,
    "chapterCount" INT,
    "quizCount" INT,
    "theoryCount" INT,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP
);

CREATE TABLE "Notification"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "notificationName" VARCHAR,
    "notificationMessage" VARCHAR,
    "userId" UUID,
    "readAt" TIMESTAMP,
    "createdAt" TIMESTAMP
);

CREATE TABLE "Enrollment"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "userId" UUID,
    "subjectId" UUID,
    "updatedAt" TIMESTAMP,
    "createdAt" TIMESTAMP,
    UNIQUE ("userId", "subjectId")
);
CREATE TABLE "EnrollmentProcess"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "enrollmentId" UUID UNIQUE,
    "process" INT,
    "chapterId" UUID,
    "status" VARCHAR,
    FOREIGN KEY ("enrollmentId") REFERENCES "Enrollment" ("id")
);


CREATE TABLE "TheoryFlashCardContent"
(
    "id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "question" VARCHAR,
    "answer" VARCHAR,
    "theoryId" UUID,
    "status" VARCHAR,
    "createdAt" TIMESTAMP,
    "updatedAt" TIMESTAMP,
    "isDeleted" BOOLEAN,
    FOREIGN KEY ("theoryId") REFERENCES "Theory" ("id")
);

-- Add foreign key constraints
ALTER TABLE "User"
    ADD FOREIGN KEY ("roleId") REFERENCES "Role" ("id");

ALTER TABLE "User"
    ADD FOREIGN KEY ("walletId") REFERENCES "Wallet" ("id");

ALTER TABLE "Notification"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Answer"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Enrollment"
    ADD FOREIGN KEY ("subjectId") REFERENCES "Subject" ("id");

ALTER TABLE "Enrollment"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Achievement"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Report"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "FlashcardContent"
    ADD FOREIGN KEY ("flashcardId") REFERENCES "Flashcard" ("id");

ALTER TABLE "Flashcard"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Transaction"
    ADD FOREIGN KEY ("subscriptionId") REFERENCES "Subscription" ("id");

ALTER TABLE "Discussion"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Discussion"
    ADD FOREIGN KEY ("subjectId") REFERENCES "Subject" ("id");

ALTER TABLE "Answer"
    ADD FOREIGN KEY ("questionId") REFERENCES "Discussion" ("id");

ALTER TABLE "Note"
    ADD FOREIGN KEY ("userId") REFERENCES "User" ("id");

ALTER TABLE "Flashcard"
    ADD FOREIGN KEY ("subjectId") REFERENCES "Subject" ("id");

ALTER TABLE "Lesson"
    ADD FOREIGN KEY ("chapterId") REFERENCES "Chapter" ("id");

ALTER TABLE "Quiz"
    ADD FOREIGN KEY ("lessonId") REFERENCES "Lesson" ("id");

ALTER TABLE "Quiz"
    ADD FOREIGN KEY ("chapterId") REFERENCES "Chapter" ("id");

ALTER TABLE "Quiz"
    ADD FOREIGN KEY ("subjectId") REFERENCES "Subject" ("id");

ALTER TABLE "Theory"
    ADD FOREIGN KEY ("lessonId") REFERENCES "Lesson" ("id");

ALTER TABLE "Chapter"
    ADD FOREIGN KEY ("subjectId") REFERENCES "Subject" ("id");


--data
INSERT INTO "Role" ("id", "roleName", "isDeleted", "createdAt", "updatedAt")
VALUES
    (uuid_generate_v4(), 'Student', false, NOW(), NOW()),
    (uuid_generate_v4(), 'Teacher', false, NOW(), NOW()),
    (uuid_generate_v4(), 'Moderator', false, NOW(), NOW()),
    (uuid_generate_v4(), 'Admin', false, NOW(), NOW());



