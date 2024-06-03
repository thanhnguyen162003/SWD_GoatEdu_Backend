create table "__EFMigrationsHistory"
(
    "MigrationId"    varchar(150) not null
        constraint "PK___EFMigrationsHistory"
            primary key,
    "ProductVersion" varchar(32)  not null
);

alter table "__EFMigrationsHistory"
    owner to root;

create table "Calculation"
(
    id             uuid default uuid_generate_v4(),
    "lessonCount"  integer,
    "chapterCount" integer,
    "quizCount"    integer,
    "theoryCount"  integer,
    "createdAt"    timestamp,
    "updatedAt"    timestamp
);

alter table "Calculation"
    owner to root;

create table "Role"
(
    id          uuid default uuid_generate_v4() not null
        constraint "PK_Role"
            primary key,
    "roleName"  varchar,
    "createdAt" timestamp,
    "updatedAt" timestamp,
    "isDeleted" boolean
);

alter table "Role"
    owner to root;

create table "Subject"
(
    id            uuid default uuid_generate_v4() not null
        constraint "PK_Subject"
            primary key,
    "subjectName" varchar,
    "subjectCode" varchar,
    information   varchar,
    class         varchar,
    image         varchar,
    "createdAt"   timestamp,
    "updatedAt"   timestamp,
    "isDeleted"   boolean
);

alter table "Subject"
    owner to root;

create table "Subscription"
(
    id                 uuid default uuid_generate_v4() not null
        constraint "PK_Subscription"
            primary key,
    "subscriptionName" varchar,
    "subscriptionBody" varchar,
    price              double precision,
    duration           interval,
    "createdAt"        timestamp,
    "isDeleted"        boolean
);

alter table "Subscription"
    owner to root;

create table "Tag"
(
    id          uuid default uuid_generate_v4() not null
        constraint "PK_Tag"
            primary key,
    "tagName"   varchar,
    "createdAt" timestamp,
    "updatedAt" timestamp,
    "isDeleted" boolean
);

alter table "Tag"
    owner to root;

create table "Wallet"
(
    id             uuid default uuid_generate_v4() not null
        constraint "PK_Wallet"
            primary key,
    "numberWallet" double precision,
    "createdAt"    timestamp,
    "updatedAt"    timestamp,
    "isDeleted"    boolean
);

alter table "Wallet"
    owner to root;

create table "Chapter"
(
    id             uuid default uuid_generate_v4() not null
        constraint "PK_Chapter"
            primary key,
    "chapterName"  varchar,
    "chapterLevel" integer,
    "subjectId"    uuid
        constraint "FK_Chapter_Subject_subjectId"
            references "Subject",
    "createdAt"    timestamp,
    "createdBy"    varchar,
    "updatedAt"    timestamp,
    "isDeleted"    boolean
);

alter table "Chapter"
    owner to root;

create index "IX_Chapter_subjectId"
    on "Chapter" ("subjectId");

create table "Transaction"
(
    id                uuid default uuid_generate_v4() not null
        constraint "PK_Transaction"
            primary key,
    "transactionName" varchar,
    note              varchar,
    "walletId"        uuid,
    "startDate"       timestamp,
    "endDate"         timestamp,
    "subscriptionId"  uuid
        constraint "FK_Transaction_Subscription_subscriptionId"
            references "Subscription",
    "createdAt"       timestamp,
    "isDeleted"       boolean
);

alter table "Transaction"
    owner to root;

create index "IX_Transaction_subscriptionId"
    on "Transaction" ("subscriptionId");

create table "User"
(
    id                uuid default uuid_generate_v4() not null
        constraint "PK_User"
            primary key,
    username          varchar,
    fullname          varchar,
    image             varchar,
    password          varchar,
    email             varchar,
    "phoneNumber"     varchar,
    subscription      varchar,
    "subscriptionEnd" timestamp,
    provider          varchar,
    "emailVerify"     boolean,
    "roleId"          uuid
        constraint "FK_User_Role_roleId"
            references "Role",
    "walletId"        uuid
        constraint "FK_User_Wallet_walletId"
            references "Wallet",
    "createdAt"       timestamp,
    "updatedAt"       timestamp,
    "isDeleted"       boolean
);

alter table "User"
    owner to root;

create index "IX_User_roleId"
    on "User" ("roleId");

create unique index "IX_User_walletId"
    on "User" ("walletId");

create table "Lesson"
(
    id               uuid default uuid_generate_v4() not null
        constraint "PK_Lesson"
            primary key,
    "lessonName"     varchar,
    "lessonBody"     varchar,
    "lessonMaterial" varchar,
    "chapterId"      uuid
        constraint "FK_Lesson_Chapter_chapterId"
            references "Chapter",
    "createdAt"      timestamp,
    "createdBy"      varchar,
    "updatedBy"      varchar,
    "updatedAt"      timestamp,
    "displayOrder"   integer,
    "isDeleted"      boolean
);

alter table "Lesson"
    owner to root;

create index "IX_Lesson_chapterId"
    on "Lesson" ("chapterId");

create table "Achievement"
(
    id                   uuid default uuid_generate_v4() not null
        constraint "PK_Achievement"
            primary key,
    "achievementName"    varchar,
    "achievementContent" varchar,
    "userId"             uuid
        constraint "FK_Achievement_User_userId"
            references "User",
    "createdAt"          timestamp,
    "isDeleted"          boolean
);

alter table "Achievement"
    owner to root;

create index "IX_Achievement_userId"
    on "Achievement" ("userId");

create table "Discussion"
(
    id                uuid default uuid_generate_v4() not null
        constraint "PK_Discussion"
            primary key,
    "discussionName"  varchar,
    "discussionBody"  varchar,
    "userId"          uuid
        constraint "FK_Discussion_User_userId"
            references "User",
    "discussionImage" varchar,
    "discussionVote"  integer,
    "subjectId"       uuid
        constraint "FK_Discussion_Subject_subjectId"
            references "Subject",
    "isSolved"        boolean,
    status            varchar,
    "createdAt"       timestamp,
    "createdBy"       varchar,
    "updatedBy"       varchar,
    "updatedAt"       timestamp,
    "isDeleted"       boolean
);

alter table "Discussion"
    owner to root;

create index "IX_Discussion_subjectId"
    on "Discussion" ("subjectId");

create index "IX_Discussion_userId"
    on "Discussion" ("userId");

create table "Enrollment"
(
    id          uuid default uuid_generate_v4() not null
        constraint "PK_Enrollment"
            primary key,
    "userId"    uuid
        constraint "FK_Enrollment_User_userId"
            references "User",
    "subjectId" uuid
        constraint "FK_Enrollment_Subject_subjectId"
            references "Subject",
    "updatedAt" timestamp,
    "createdAt" timestamp
);

alter table "Enrollment"
    owner to root;

create unique index "Enrollment_userId_subjectId_key"
    on "Enrollment" ("userId", "subjectId");

create index "IX_Enrollment_subjectId"
    on "Enrollment" ("subjectId");

create table "Flashcard"
(
    id                     uuid default uuid_generate_v4() not null
        constraint "PK_Flashcard"
            primary key,
    "flashcardName"        varchar,
    "flashcardDescription" varchar,
    "userId"               uuid
        constraint "FK_Flashcard_User_userId"
            references "User",
    "subjectId"            uuid
        constraint "FK_Flashcard_Subject_subjectId"
            references "Subject",
    status                 varchar,
    star                   integer,
    "createdAt"            timestamp,
    "createdBy"            varchar,
    "updatedBy"            varchar,
    "updatedAt"            timestamp,
    "isDeleted"            boolean
);

alter table "Flashcard"
    owner to root;

create index "IX_Flashcard_subjectId"
    on "Flashcard" ("subjectId");

create index "IX_Flashcard_userId"
    on "Flashcard" ("userId");

create table "Note"
(
    id          uuid default uuid_generate_v4() not null
        constraint "PK_Note"
            primary key,
    "noteName"  varchar,
    "noteBody"  varchar,
    "userId"    uuid
        constraint "FK_Note_User_userId"
            references "User",
    "createdAt" timestamp,
    "createdBy" varchar,
    "updatedBy" varchar,
    "updatedAt" timestamp,
    "isDeleted" boolean
);

alter table "Note"
    owner to root;

create index "IX_Note_userId"
    on "Note" ("userId");

create table "Notification"
(
    id                    uuid default uuid_generate_v4() not null
        constraint "PK_Notification"
            primary key,
    "notificationName"    varchar,
    "notificationMessage" varchar,
    "userId"              uuid
        constraint "FK_Notification_User_userId"
            references "User",
    "readAt"              timestamp,
    "createdAt"           timestamp
);

alter table "Notification"
    owner to root;

create index "IX_Notification_userId"
    on "Notification" ("userId");

create table "Report"
(
    id              uuid default uuid_generate_v4() not null
        constraint "PK_Report"
            primary key,
    "reportTitle"   varchar,
    "reportContent" varchar,
    "userId"        uuid
        constraint "FK_Report_User_userId"
            references "User",
    "createdAt"     timestamp,
    "createdBy"     varchar,
    status          varchar,
    "isDeleted"     boolean
);

alter table "Report"
    owner to root;

create index "IX_Report_userId"
    on "Report" ("userId");

create table "Quiz"
(
    id          uuid default uuid_generate_v4() not null
        constraint "PK_Quiz"
            primary key,
    quiz        varchar,
    "quizLevel" integer,
    "lessonId"  uuid
        constraint "FK_Quiz_Lesson_lessonId"
            references "Lesson",
    "chapterId" uuid
        constraint "FK_Quiz_Chapter_chapterId"
            references "Chapter",
    "subjectId" uuid
        constraint "FK_Quiz_Subject_subjectId"
            references "Subject",
    "createdAt" timestamp,
    "updatedAt" timestamp,
    "isRequire" boolean,
    "isDeleted" boolean
);

alter table "Quiz"
    owner to root;

create index "IX_Quiz_chapterId"
    on "Quiz" ("chapterId");

create index "IX_Quiz_lessonId"
    on "Quiz" ("lessonId");

create index "IX_Quiz_subjectId"
    on "Quiz" ("subjectId");

create table "Theory"
(
    id              uuid default uuid_generate_v4() not null
        constraint "PK_Theory"
            primary key,
    "theoryName"    varchar,
    file            varchar,
    image           varchar,
    "theoryContent" varchar,
    "lessonId"      uuid
        constraint "FK_Theory_Lesson_lessonId"
            references "Lesson",
    "createdAt"     timestamp,
    "updatedAt"     timestamp,
    "isDeleted"     boolean
);

alter table "Theory"
    owner to root;

create index "IX_Theory_lessonId"
    on "Theory" ("lessonId");

create table "Answer"
(
    id            uuid default uuid_generate_v4() not null
        constraint "PK_Answer"
            primary key,
    "answerName"  varchar,
    "answerBody"  varchar,
    "userId"      uuid
        constraint "FK_Answer_User_userId"
            references "User",
    "questionId"  uuid
        constraint "FK_Answer_Discussion_questionId"
            references "Discussion",
    "answerImage" varchar,
    "answerVote"  integer,
    "createdAt"   timestamp,
    "createdBy"   varchar,
    "updatedBy"   varchar,
    "updatedAt"   timestamp,
    "isDeleted"   boolean
);

alter table "Answer"
    owner to root;

create index "IX_Answer_questionId"
    on "Answer" ("questionId");

create index "IX_Answer_userId"
    on "Answer" ("userId");

create table "DiscussionTag"
(
    "DiscussionsId" uuid not null
        constraint "FK_DiscussionTag_Discussion_DiscussionsId"
            references "Discussion"
            on delete cascade,
    "TagsId"        uuid not null
        constraint "FK_DiscussionTag_Tag_TagsId"
            references "Tag"
            on delete cascade,
    constraint "PK_DiscussionTag"
        primary key ("DiscussionsId", "TagsId")
);

alter table "DiscussionTag"
    owner to root;

create index "IX_DiscussionTag_TagsId"
    on "DiscussionTag" ("TagsId");

create table "EnrollmentProcess"
(
    id             uuid default uuid_generate_v4() not null
        constraint "PK_EnrollmentProcess"
            primary key,
    "enrollmentId" uuid
        constraint "FK_EnrollmentProcess_Enrollment_enrollmentId"
            references "Enrollment",
    process        integer,
    "chapterId"    uuid,
    status         varchar
);

alter table "EnrollmentProcess"
    owner to root;

create unique index "EnrollmentProcess_enrollmentId_key"
    on "EnrollmentProcess" ("enrollmentId");

create table "FlashcardContent"
(
    id                         uuid default uuid_generate_v4() not null
        constraint "PK_FlashcardContent"
            primary key,
    "flashcardContentQuestion" varchar,
    "flashcardContentAnswer"   varchar,
    "flashcardId"              uuid
        constraint "FK_FlashcardContent_Flashcard_flashcardId"
            references "Flashcard",
    image                      varchar,
    status                     varchar,
    "createdAt"                timestamp,
    "createdBy"                varchar,
    "updatedBy"                varchar,
    "updatedAt"                timestamp,
    "isDeleted"                boolean
);

alter table "FlashcardContent"
    owner to root;

create index "IX_FlashcardContent_flashcardId"
    on "FlashcardContent" ("flashcardId");

create table "FlashcardTag"
(
    "FlashcardsId" uuid not null
        constraint "FK_FlashcardTag_Flashcard_FlashcardsId"
            references "Flashcard"
            on delete cascade,
    "TagsId"       uuid not null
        constraint "FK_FlashcardTag_Tag_TagsId"
            references "Tag"
            on delete cascade,
    constraint "PK_FlashcardTag"
        primary key ("FlashcardsId", "TagsId")
);

alter table "FlashcardTag"
    owner to root;

create index "IX_FlashcardTag_TagsId"
    on "FlashcardTag" ("TagsId");

create table "QuestionInQuiz"
(
    id             uuid default uuid_generate_v4() not null
        constraint "PK_QuestionInQuiz"
            primary key,
    "quizId"       uuid
        constraint "FK_QuestionInQuiz_Quiz_quizId"
            references "Quiz",
    "quizQuestion" varchar,
    "quizAnswer1"  varchar,
    "quizAnswer2"  varchar,
    "quizAnswer3"  varchar,
    "quizCorrect"  varchar,
    "createdAt"    timestamp,
    "updatedAt"    timestamp,
    "isDeleted"    boolean
);

alter table "QuestionInQuiz"
    owner to root;

create index "IX_QuestionInQuiz_quizId"
    on "QuestionInQuiz" ("quizId");

create table "TheoryFlashCardContent"
(
    id          uuid default uuid_generate_v4() not null
        constraint "PK_TheoryFlashCardContent"
            primary key,
    question    varchar,
    answer      varchar,
    "theoryId"  uuid
        constraint "FK_TheoryFlashCardContent_Theory_theoryId"
            references "Theory",
    status      varchar,
    "createdAt" timestamp,
    "updatedAt" timestamp,
    "isDeleted" boolean
);

alter table "TheoryFlashCardContent"
    owner to root;

create index "IX_TheoryFlashCardContent_theoryId"
    on "TheoryFlashCardContent" ("theoryId");

create table "Votes"
(
    id              integer generated by default as identity
        constraint "PK_Votes"
            primary key,
    "discussionId"  uuid
        constraint "FK_Votes_Discussion_discussionId"
            references "Discussion",
    "answerId"      uuid
        constraint "FK_Votes_Answer_answerId"
            references "Answer",
    "userId"        uuid
        constraint "FK_Votes_User_userId"
            references "User",
    "voteValue"     smallint,
    "voteTimeStamp" timestamp default CURRENT_TIMESTAMP
);

alter table "Votes"
    owner to root;

create index "IX_Votes_answerId"
    on "Votes" ("answerId");

create index "IX_Votes_discussionId"
    on "Votes" ("discussionId");

create index "IX_Votes_userId"
    on "Votes" ("userId");




--data
INSERT INTO "Role" ("id", "roleName", "isDeleted", "createdAt", "updatedAt")
VALUES
    (uuid_generate_v4(), 'Student', false, NOW(), NOW()),
    (uuid_generate_v4(), 'Teacher', false, NOW(), NOW()),
    (uuid_generate_v4(), 'Moderator', false, NOW(), NOW()),
    (uuid_generate_v4(), 'Admin', false, NOW(), NOW());


--tag
INSERT INTO "Tag" ("tagName", "createdAt", "updatedAt", "isDeleted")
VALUES
    ('study_material', NOW(), NOW(), false),
    ('exam_preparation', NOW(), NOW(), false),
    ('study_tips', NOW(), NOW(), false),
    ('exam_tips', NOW(), NOW(), false),
    ('study_schedule', NOW(), NOW(), false),
    ('exam_schedule', NOW(), NOW(), false),
    ('study_group', NOW(), NOW(), false),
    ('exam_notes', NOW(), NOW(), false),
    ('study_guide', NOW(), NOW(), false),
    ('exam_guide', NOW(), NOW(), false),
    ('study_resources', NOW(), NOW(), false),
    ('exam_resources', NOW(), NOW(), false),
    ('study_plan', NOW(), NOW(), false),
    ('exam_plan', NOW(), NOW(), false),
    ('study_strategies', NOW(), NOW(), false),
    ('exam_strategies', NOW(), NOW(), false),
    ('study_habits', NOW(), NOW(), false),
    ('exam_habits', NOW(), NOW(), false),
    ('study_timetable', NOW(), NOW(), false),
    ('exam_timetable', NOW(), NOW(), false),
    ('study_tools', NOW(), NOW(), false),
    ('exam_tools', NOW(), NOW(), false),
    ('study_aids', NOW(), NOW(), false),
    ('exam_aids', NOW(), NOW(), false),
    ('study_techniques', NOW(), NOW(), false),
    ('exam_techniques', NOW(), NOW(), false),
    ('study_tactics', NOW(), NOW(), false),
    ('exam_tactics', NOW(), NOW(), false),
    ('study_methods', NOW(), NOW(), false),
    ('exam_methods', NOW(), NOW(), false),
    ('study_sessions', NOW(), NOW(), false),
    ('exam_sessions', NOW(), NOW(), false),
    ('study_checklist', NOW(), NOW(), false),
    ('exam_checklist', NOW(), NOW(), false),
    ('study_motivation', NOW(), NOW(), false),
    ('exam_motivation', NOW(), NOW(), false),
    ('study_focus', NOW(), NOW(), false),
    ('exam_focus', NOW(), NOW(), false),
    ('study_routine', NOW(), NOW(), false),
    ('exam_routine', NOW(), NOW(), false),
    ('study_prep', NOW(), NOW(), false),
    ('exam_prep', NOW(), NOW(), false),
    ('study_exercises', NOW(), NOW(), false),
    ('exam_exercises', NOW(), NOW(), false),
    ('study_reviews', NOW(), NOW(), false),
    ('exam_reviews', NOW(), NOW(), false),
    ('study_revisions', NOW(), NOW(), false),
    ('exam_revisions', NOW(), NOW(), false),
    ('study_topics', NOW(), NOW(), false),
    ('exam_topics', NOW(), NOW(), false),
    ('study_sessions', NOW(), NOW(), false),
    ('exam_sessions', NOW(), NOW(), false),
    ('study_materials', NOW(), NOW(), false),
    ('exam_materials', NOW(), NOW(), false),
    ('study_handouts', NOW(), NOW(), false),
    ('exam_handouts', NOW(), NOW(), false),
    ('study_outline', NOW(), NOW(), false),
    ('exam_outline', NOW(), NOW(), false),
    ('study_chapter', NOW(), NOW(), false),
    ('exam_chapter', NOW(), NOW(), false),
    ('study_quiz', NOW(), NOW(), false),
    ('exam_quiz', NOW(), NOW(), false),
    ('study_test', NOW(), NOW(), false),
    ('exam_test', NOW(), NOW(), false),
    ('study_practice', NOW(), NOW(), false),
    ('exam_practice', NOW(), NOW(), false),
    ('study_mock', NOW(), NOW(), false),
    ('exam_mock', NOW(), NOW(), false),
    ('study_trial', NOW(), NOW(), false),
    ('exam_trial', NOW(), NOW(), false),
    ('study_review', NOW(), NOW(), false),
    ('exam_review', NOW(), NOW(), false),
    ('study_recall', NOW(), NOW(), false),
    ('exam_recall', NOW(), NOW(), false),
    ('study_revision', NOW(), NOW(), false),
    ('exam_revision', NOW(), NOW(), false),
    ('study_session', NOW(), NOW(), false),
    ('exam_session', NOW(), NOW(), false),
    ('study_day', NOW(), NOW(), false),
    ('exam_day', NOW(), NOW(), false),
    ('study_week', NOW(), NOW(), false),
    ('exam_week', NOW(), NOW(), false),
    ('study_month', NOW(), NOW(), false),
    ('exam_month', NOW(), NOW(), false),
    ('study_year', NOW(), NOW(), false),
    ('exam_year', NOW(), NOW(), false),
    ('study_prep_material', NOW(), NOW(), false),
    ('exam_prep_material', NOW(), NOW(), false),
    ('study_support', NOW(), NOW(), false),
    ('exam_support', NOW(), NOW(), false),
    ('study_assistance', NOW(), NOW(), false),
    ('exam_assistance', NOW(), NOW(), false),
    ('study_partner', NOW(), NOW(), false),
    ('exam_partner', NOW(), NOW(), false),
    ('study_buddy', NOW(), NOW(), false),
    ('exam_buddy', NOW(), NOW(), false),
    ('study_buddy_system', NOW(), NOW(), false),
    ('exam_buddy_system', NOW(), NOW(), false),
    ('study_session_notes', NOW(), NOW(), false),
    ('exam_session_notes', NOW(), NOW(), false),
    ('study_session_tips', NOW(), NOW(), false),
    ('exam_session_tips', NOW(), NOW(), false),
    ('study_group_tips', NOW(), NOW(), false),
    ('exam_group_tips', NOW(), NOW(), false),
    ('study_group_notes', NOW(), NOW(), false),
    ('exam_group_notes', NOW(), NOW(), false);
('study_outline', NOW(), NOW(), false),
('exam_outline', NOW(), NOW(), false),
('study_handouts', NOW(), NOW(), false),
('exam_handouts', NOW(), NOW(), false),
('study_chapter', NOW(), NOW(), false),
('exam_chapter', NOW(), NOW(), false),
('study_quiz', NOW(), NOW(), false),
('exam_quiz', NOW(), NOW(), false),
('study_test', NOW(), NOW(), false),
('exam_test', NOW(), NOW(), false),
('study_practice', NOW(), NOW(), false),
('exam_practice', NOW(), NOW(), false),
('study_mock', NOW(), NOW(), false),
('exam_mock', NOW(), NOW(), false),
('study_trial', NOW(), NOW(), false),
('exam_trial', NOW(), NOW(), false),
('study_review', NOW(), NOW(), false),
('exam_review', NOW(), NOW(), false),
('study_recall', NOW(), NOW(), false),
('exam_recall', NOW(), NOW(), false),
('study_revision', NOW(), NOW(), false),
('exam_revision', NOW(), NOW(), false),
('study_session', NOW(), NOW(), false),
('exam_session', NOW(), NOW(), false),
('study_day', NOW(), NOW(), false),
('exam_day', NOW(), NOW(), false),
('study_week', NOW(), NOW(), false),
('exam_week', NOW(), NOW(), false),
('study_month', NOW(), NOW(), false),
('exam_month', NOW(), NOW(), false),
('study_year', NOW(), NOW(), false),
('exam_year', NOW(), NOW(), false),
('study_prep_material', NOW(), NOW(), false),
('exam_prep_material', NOW(), NOW(), false),
('study_support', NOW(), NOW(), false),
('exam_support', NOW(), NOW(), false),
('study_assistance', NOW(), NOW(), false),
('exam_assistance', NOW(), NOW(), false),
('study_partner', NOW(), NOW(), false),
('exam_partner', NOW(), NOW(), false),
('study_buddy', NOW(), NOW(), false),
('exam_buddy', NOW(), NOW(), false),
('study_buddy_system', NOW(), NOW(), false),
('exam_buddy_system', NOW(), NOW(), false),
('study_session_notes', NOW(), NOW(), false),
('exam_session_notes', NOW(), NOW(), false),
('study_session_tips', NOW(), NOW(), false),
('exam_session_tips', NOW(), NOW(), false),
('study_group_tips', NOW(), NOW(), false),
('exam_group_tips', NOW(), NOW(), false),
('study_group_notes', NOW(), NOW(), false),
('exam_group_notes', NOW(), NOW(), false),
('study_resources_online', NOW(), NOW(), false),
('exam_resources_online', NOW(), NOW(), false),
('study_resources_library', NOW(), NOW(), false),
('exam_resources_library', NOW(), NOW(), false),
('study_resources_books', NOW(), NOW(), false),
('exam_resources_books', NOW(), NOW(), false),
('study_resources_videos', NOW(), NOW(), false),
('exam_resources_videos', NOW(), NOW(), false),
('study_resources_articles', NOW(), NOW(), false),
('exam_resources_articles', NOW(), NOW(), false),
('study_flashcards', NOW(), NOW(), false),
('exam_flashcards', NOW(), NOW(), false),
('study_notes', NOW(), NOW(), false),
('exam_notes', NOW(), NOW(), false),
('study_cheat_sheets', NOW(), NOW(), false),
('exam_cheat_sheets', NOW(), NOW(), false),
('study_guides', NOW(), NOW(), false),
('exam_guides', NOW(), NOW(), false),
('study_planners', NOW(), NOW(), false),
('exam_planners', NOW(), NOW(), false),
('study_calendar', NOW(), NOW(), false),
('exam_calendar', NOW(), NOW(), false),
('study_reminders', NOW(), NOW(), false),
('exam_reminders', NOW(), NOW(), false),
('study_notifications', NOW(), NOW(), false),
('exam_notifications', NOW(), NOW(), false),
('study_alerts', NOW(), NOW(), false),
('exam_alerts', NOW(), NOW(), false),
('study_preparation_tips', NOW(), NOW(), false),
('exam_preparation_tips', NOW(), NOW(), false),
('study_material_checklist', NOW(), NOW(), false),
('exam_material_checklist', NOW(), NOW(), false),
('study_folders', NOW(), NOW(), false),
('exam_folders', NOW(), NOW(), false),
('study_organizers', NOW(), NOW(), false),
('exam_organizers', NOW(), NOW(), false),
('study_timers', NOW(), NOW(), false),
('exam_timers', NOW(), NOW(), false),
('study_planning_tools', NOW(), NOW(), false),
('exam_planning_tools', NOW(), NOW(), false),
('study_time_management', NOW(), NOW(), false),
('exam_time_management', NOW(), NOW(), false),
('study_practice_exams', NOW(), NOW(), false),
('exam_practice_exams', NOW(), NOW(), false),
('study_sample_tests', NOW(), NOW(), false),
('exam_sample_tests', NOW(), NOW(), false),
('study_worksheets', NOW(), NOW(), false),
('exam_worksheets', NOW(), NOW(), false),
('study_assignments', NOW(), NOW(), false),
('exam_assignments', NOW(), NOW(), false),
('study_homework', NOW(), NOW(), false),
('exam_homework', NOW(), NOW(), false),
('study_lab_reports', NOW(), NOW(), false),
('exam_lab_reports', NOW(), NOW(), false),
('study_project_reports', NOW(), NOW(), false),
('exam_project_reports', NOW(), NOW(), false),
('study_thesis', NOW(), NOW(), false),
('exam_thesis', NOW(), NOW(), false),
('study_dissertation', NOW(), NOW(), false),
('exam_dissertation', NOW(), NOW(), false),
('study_research_papers', NOW(), NOW(), false),
('exam_research_papers', NOW(), NOW(), false),
('study_literature_review', NOW(), NOW(), false),
('exam_literature_review', NOW(), NOW(), false),
('study_presentation', NOW(), NOW(), false),
('exam_presentation', NOW(), NOW(), false),
('study_slides', NOW(), NOW(), false),
('exam_slides', NOW(), NOW(), false),
('study_handouts', NOW(), NOW(), false),
('exam_handouts', NOW(), NOW(), false),
('study_textbooks', NOW(), NOW(), false),
('exam_textbooks', NOW(), NOW(), false),
('study_ebooks', NOW(), NOW(), false),
('exam_ebooks', NOW(), NOW(), false),
('study_pdfs', NOW(), NOW(), false),
('exam_pdfs', NOW(), NOW(), false),
('study_docs', NOW(), NOW(), false),
('exam_docs', NOW(), NOW(), false),
('study_sheets', NOW(), NOW(), false),
('exam_sheets', NOW(), NOW(), false),
('study_charts', NOW(), NOW(), false),
('exam_charts', NOW(), NOW(), false),
('study_graphs', NOW(), NOW(), false),
('exam_graphs', NOW(), NOW(), false),
('study_data', NOW(), NOW(), false),
('exam_data', NOW(), NOW(), false),
('study_statistics', NOW(), NOW(), false),
('exam_statistics', NOW(), NOW(), false),
('study_figures', NOW(), NOW(), false),
('exam_figures', NOW(), NOW(), false),
('study_tables', NOW(), NOW(), false),
('exam_tables', NOW(), NOW(), false),
('study_diagrams', NOW(), NOW(), false),
('exam_diagrams', NOW(), NOW(), false),
('study_equations', NOW(), NOW(), false),
('exam_equations', NOW(), NOW(), false),
('study_formulas', NOW(), NOW(), false),
('exam_formulas', NOW(), NOW(), false),
('study_concepts', NOW(), NOW(), false),
('exam_concepts', NOW(), NOW(), false),
('study_definitions', NOW(), NOW(), false),
('exam_definitions', NOW(), NOW(), false),
('study_glossary', NOW(), NOW(), false),
('exam_glossary', NOW(), NOW(), false),
('study_vocabulary', NOW(), NOW(), false),
('exam_vocabulary', NOW(), NOW(), false),
('study_terms', NOW(), NOW(), false),
('exam_terms', NOW(), NOW(), false),
('study_summaries', NOW(), NOW(), false),
('exam_summaries', NOW(), NOW(), false),
('study_outlines', NOW(), NOW(), false),
('exam_outlines', NOW(), NOW(), false),
('study_overviews', NOW(), NOW(), false),
('exam_overviews', NOW(), NOW(), false),
('study_briefs', NOW(), NOW(), false),
('exam_briefs', NOW(), NOW(), false),
('study_highlights', NOW(), NOW(), false),
('exam_highlights', NOW(), NOW(), false),
('study_notes_summary', NOW(), NOW(), false),
('exam_notes_summary', NOW(), NOW(), false),
('study_review_sheets', NOW(), NOW(), false),
('exam_review_sheets', NOW(), NOW(), false),
('study_guides_notes', NOW(), NOW(), false),
('exam_guides_notes', NOW(), NOW(), false),
('study_blueprints', NOW(), NOW(), false),
('exam_blueprints', NOW(), NOW(), false),
('study_maps', NOW(), NOW(), false),
('exam_maps', NOW(), NOW(), false),
('study_paths', NOW(), NOW(), false),
('exam_paths', NOW(), NOW(), false),
('study_roadmaps', NOW(), NOW(), false),
('exam_roadmaps', NOW(), NOW(), false),
('study_plans', NOW(), NOW(), false),
('exam_plans', NOW(), NOW(), false),
('study_agenda', NOW(), NOW(), false),
('exam_agenda', NOW(), NOW(), false),
('study_schedules', NOW(), NOW(), false),
('exam_schedules', NOW(), NOW(), false),
('study_routines', NOW(), NOW(), false),
('exam_routines', NOW(), NOW(), false),
('study_timetables', NOW(), NOW(), false),
('exam_timetables', NOW(), NOW(), false),
('study_sessions', NOW(), NOW(), false),
('exam_sessions', NOW(), NOW(), false),
('study_prep_guides', NOW(), NOW(), false),
('exam_prep_guides', NOW(), NOW(), false),
('study_kits', NOW(), NOW(), false),
('exam_kits', NOW(), NOW(), false),
('study_tools_kits', NOW(), NOW(), false),
('exam_tools_kits', NOW(), NOW(), false),
('study_bundles', NOW(), NOW(), false),
('exam_bundles', NOW(), NOW(), false),
('study_sets', NOW(), NOW(), false),
('exam_sets', NOW(), NOW(), false),
('study_collections', NOW(), NOW(), false),
('exam_collections', NOW(), NOW(), false),
('study_packs', NOW(), NOW(), false),
('exam_packs', NOW(), NOW(), false),
('study_combos', NOW(), NOW(), false),
('exam_combos', NOW(), NOW(), false),
('study_essentials', NOW(), NOW(), false),
('exam_essentials', NOW(), NOW(), false),
('study_must_haves', NOW(), NOW(), false),
('exam_must_haves', NOW(), NOW(), false),
('study_basics', NOW(), NOW(), false),
('exam_basics', NOW(), NOW(), false),
('study_fundamentals', NOW(), NOW(), false),
('exam_fundamentals', NOW(), NOW(), false),
('study_core', NOW(), NOW(), false),
('exam_core', NOW(), NOW(), false),
('study_principles', NOW(), NOW(), false),
('exam_principles', NOW(), NOW(), false),
('study_knowledge', NOW(), NOW(), false),
('exam_knowledge', NOW(), NOW(), false),
('study_skills', NOW(), NOW(), false),
('exam_skills', NOW(), NOW(), false),
('study_abilities', NOW(), NOW(), false),
('exam_abilities', NOW(), NOW(), false),
('study_expertise', NOW(), NOW(), false),
('exam_expertise', NOW(), NOW(), false),
('study_proficiency', NOW(), NOW(), false),
('exam_proficiency', NOW(), NOW(), false),
('study_competency', NOW(), NOW(), false),
('exam_competency', NOW(), NOW(), false),
('study_aptitude', NOW(), NOW(), false),
('exam_aptitude', NOW(), NOW(), false),
('study_capability', NOW(), NOW(), false),
('exam_capability', NOW(), NOW(), false),
('study_capacity', NOW(), NOW(), false),
('exam_capacity', NOW(), NOW(), false),
('study_strength', NOW(), NOW(), false),
('exam_strength', NOW(), NOW(), false),
('study_endurance', NOW(), NOW(), false),
('exam_endurance', NOW(), NOW(), false),
('study_resilience', NOW(), NOW(), false),
('exam_resilience', NOW(), NOW(), false),
('study_focus_areas', NOW(), NOW(), false),
('exam_focus_areas', NOW(), NOW(), false),
('study_key_topics', NOW(), NOW(), false),
('exam_key_topics', NOW(), NOW(), false),
('study_main_points', NOW(), NOW(), false),
('exam_main_points', NOW(), NOW(), false),
('study_important_concepts', NOW(), NOW(), false),
('exam_important_concepts', NOW(), NOW(), false),
('study_critical_points', NOW(), NOW(), false),
('exam_critical_points', NOW(), NOW(), false),
('study_essential_material', NOW(), NOW(), false),
('exam_essential_material', NOW(), NOW(), false),
('study_core_material', NOW(), NOW(), false),
('exam_core_material', NOW(), NOW(), false),
('study_primary_sources', NOW(), NOW(), false),
('exam_primary_sources', NOW(), NOW(), false),
('study_secondary_sources', NOW(), NOW(), false),
('exam_secondary_sources', NOW(), NOW(), false),
('study_reference_material', NOW(), NOW(), false),
('exam_reference_material', NOW(), NOW(), false),
('study_background_material', NOW(), NOW(), false),
('exam_background_material', NOW(), NOW(), false),
('study_context_material', NOW(), NOW(), false),
('exam_context_material', NOW(), NOW(), false),
('study_supplementary_material', NOW(), NOW(), false),
('exam_supplementary_material', NOW(), NOW(), false),
('study_additional_material', NOW(), NOW(), false),
('exam_additional_material', NOW(), NOW(), false),
('study_supporting_material', NOW(), NOW(), false),
('exam_supporting_material', NOW(), NOW(), false),
('study_relevant_material', NOW(), NOW(), false),
('exam_relevant_material', NOW(), NOW(), false),
('study_related_material', NOW(), NOW(), false),
('exam_related_material', NOW(), NOW(), false),
('study_connected_material', NOW(), NOW(), false),
('exam_connected_material', NOW(), NOW(), false),
('study_associated_material', NOW(), NOW(), false),
('exam_associated_material', NOW(), NOW(), false),
('study_linked_material', NOW(), NOW(), false),
('exam_linked_material', NOW(), NOW(), false),
('study_interlinked_material', NOW(), NOW(), false),
('exam_interlinked_material', NOW(), NOW(), false),
('study_cross_referenced_material', NOW(), NOW(), false),
('exam_cross_referenced_material', NOW(), NOW(), false),
('study_cited_material', NOW(), NOW(), false),
('exam_cited_material', NOW(), NOW(), false),
('study_quick_references', NOW(), NOW(), false),
('exam_quick_references', NOW(), NOW(), false),
('study_cheat_sheets_guides', NOW(), NOW(), false),
('exam_cheat_sheets_guides', NOW(), NOW(), false),
('study_cheat_sheets_material', NOW(), NOW(), false),
('exam_cheat_sheets_material', NOW(), NOW(), false),
('study_cheat_sheets_notes', NOW(), NOW(), false),
('exam_cheat_sheets_notes', NOW(), NOW(), false);



--subject
INSERT INTO "Subject" ("subjectName", "subjectCode", "information", "class", "image", "createdAt", "updatedAt", "isDeleted")
VALUES
    ('Mathematics', 'MATH101', 'Basic concepts of algebra and geometry', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Physics', 'PHYS101', 'Introduction to mechanics and thermodynamics', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Chemistry', 'CHEM101', 'Basic principles of chemistry and laboratory techniques', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Biology', 'BIO101', 'Fundamentals of biology including cell structure and function', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('English Literature', 'ENG101', 'Study of English prose, poetry, and drama', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('History', 'HIST101', 'Overview of ancient and modern world history', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Geography', 'GEO101', 'Introduction to physical and human geography', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Computer Science', 'CS101', 'Basics of computer programming and algorithms', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Economics', 'ECO101', 'Principles of microeconomics and macroeconomics', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Psychology', 'PSY101', 'Introduction to psychological theories and practices', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Sociology', 'SOC101', 'Study of society, social institutions, and relationships', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Philosophy', 'PHIL101', 'Exploration of fundamental philosophical questions', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Political Science', 'POL101', 'Introduction to political systems and theories', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Art History', 'ARTH101', 'Study of major art movements and artists', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Music Theory', 'MUS101', 'Fundamentals of music theory and composition', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Physical Education', 'PE101', 'Principles of physical fitness and health', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Environmental Science', 'ENV101', 'Study of environmental issues and conservation', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Business Studies', 'BUS101', 'Basics of business management and entrepreneurship', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Accounting', 'ACC101', 'Introduction to financial accounting and reporting', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Marketing', 'MKT101', 'Principles of marketing and consumer behavior', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Statistics', 'STAT101', 'Basics of statistical methods and data analysis', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Engineering', 'ENGR101', 'Fundamentals of engineering principles and practices', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Law', 'LAW101', 'Introduction to legal principles and systems', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Medicine', 'MED101', 'Basics of human anatomy and physiology', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Nursing', 'NUR101', 'Principles of nursing and patient care', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Education', 'EDU101', 'Introduction to educational theories and practices', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Architecture', 'ARCH101', 'Basics of architectural design and history', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Journalism', 'JOUR101', 'Principles of news reporting and media ethics', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Film Studies', 'FILM101', 'Introduction to film theory and criticism', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Theatre Arts', 'THR101', 'Fundamentals of acting and stage production', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Graphic Design', 'GD101', 'Basics of visual design and digital media', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Creative Writing', 'CW101', 'Techniques and styles of creative writing', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Anthropology', 'ANTH101', 'Study of human societies and cultures', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Astronomy', 'ASTR101', 'Introduction to the study of the universe', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Linguistics', 'LING101', 'Basics of language structure and function', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Classical Studies', 'CLAS101', 'Study of ancient Greek and Roman civilizations', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Religious Studies', 'REL101', 'Exploration of world religions and beliefs', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Public Administration', 'PUB101', 'Principles of public sector management', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Urban Planning', 'URB101', 'Introduction to urban development and planning', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('International Relations', 'IR101', 'Study of global political and economic systems', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Agriculture', 'AGRI101', 'Principles of agricultural science and technology', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Veterinary Science', 'VET101', 'Introduction to animal health and care', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Astronautics', 'ASTRO101', 'Basics of space exploration and technology', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Marine Biology', 'MARBIO101', 'Study of marine organisms and ecosystems', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Sports Management', 'SPM101', 'Principles of sports administration and marketing', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Culinary Arts', 'CUL101', 'Fundamentals of cooking and food preparation', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Fashion Design', 'FASH101', 'Basics of fashion design and textile science', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Hospitality Management', 'HOSP101', 'Principles of hotel and tourism management', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Forestry', 'FOR101', 'Study of forest ecosystems and conservation', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Geology', 'GEOL101', 'Basics of earth science and geological processes', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Meteorology', 'MET101', 'Introduction to weather and climate science', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Microbiology', 'MICRO101', 'Study of microorganisms and their effects', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Genetics', 'GEN101', 'Basics of genetic inheritance and variation', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Neuroscience', 'NEURO101', 'Introduction to the structure and function of the nervous system', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Pathology', 'PATH101', 'Study of disease causes and effects', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Pharmacology', 'PHARM101', 'Basics of drug action and therapeutic use', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Toxicology', 'TOX101', 'Study of harmful effects of substances on living organisms', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Immunology', 'IMMUN101', 'Basics of the immune system and its functions', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Virology', 'VIRO101', 'Study of viruses and viral diseases', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Biotechnology', 'BIOTECH101', 'Introduction to the use of living systems in technology', 'Class 1', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Ecology', 'ECOLOGY101', 'Study of organisms and their environments', 'Class 2', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Zoology', 'ZOO101', 'Study of animal biology and behavior', 'Class 3', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false),
    ('Botany', 'BOT101', 'Basics of plant biology and physiology', 'Class 4', 'https://cdn.getmidnight.com/45d07b00b0188a892509950ff919e14e/2021/09/B_E19-title.png', NOW(), NOW(), false);


