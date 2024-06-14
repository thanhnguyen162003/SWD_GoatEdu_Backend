
CREATE INDEX idx_subject_isdeleted ON "Subject" ("isDeleted");

CREATE INDEX idx_subject_id_createdat ON "Subject" (id, "createdAt");

CREATE INDEX idx_subject_subjectname ON "Subject" ("subjectName");

CREATE INDEX idx_chapter_subjectid ON "Chapter" ("subjectId");

CREATE INDEX idx_subject_createdat_subjectname_id ON "Subject" ("createdAt", "subjectName", id);

CREATE INDEX idx_subject_isDeleted ON "Subject" ("isDeleted");
CREATE INDEX idx_chapter_subjectId_isDeleted ON "Chapter" ("subjectId", "isDeleted");
CREATE INDEX idx_subject_ordering ON "Subject" ("createdAt", "subjectName", "id");

-- Indexes for Discussion table
CREATE INDEX idx_discussion_isdeleted ON "Discussion" ("isDeleted");
CREATE INDEX idx_discussion_status ON "Discussion" (status);
CREATE INDEX idx_discussion_userid ON "Discussion" ("userId");
CREATE INDEX idx_discussion_subjectid ON "Discussion" ("subjectId");
CREATE INDEX idx_discussion_createdat ON "Discussion" ("createdAt");

-- Indexes for User table
CREATE INDEX idx_user_id ON "User" (id);

-- Indexes for Subject table
CREATE INDEX idx_subject_id ON "Subject" (id);

-- Indexes for Tag table
CREATE INDEX idx_tag_id ON "Tag" (id);

-- Indexes for Votes table
CREATE INDEX idx_votes_discussionid ON "Votes" ("discussionId");



-- Remove indexes
DROP INDEX IF EXISTS idx_subject_isdeleted;
DROP INDEX IF EXISTS idx_subject_id_createdat;
DROP INDEX IF EXISTS idx_subject_subjectname;
DROP INDEX IF EXISTS idx_chapter_subjectid;
DROP INDEX IF EXISTS idx_subject_createdat_subjectname_id;

EXPLAIN ANALYZE
SELECT s.id, s.class, s."createdAt", s.image, s.information, s."isDeleted", s."subjectCode", s."subjectName", s."updatedAt", c.id, c."chapterLevel", c."chapterName", c."createdAt", c."createdBy", c."isDeleted", c."subjectId", c."updatedAt"
FROM "Subject" AS s
         LEFT JOIN "Chapter" AS c ON s.id = c."subjectId"
WHERE s."isDeleted" = FALSE
ORDER BY s."createdAt" DESC, s."subjectName", s.id;

.