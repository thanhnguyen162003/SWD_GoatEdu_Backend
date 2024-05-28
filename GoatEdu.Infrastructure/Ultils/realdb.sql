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

