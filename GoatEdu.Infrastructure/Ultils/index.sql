
CREATE INDEX idx_subject_isdeleted ON "Subject" ("isDeleted");

CREATE INDEX idx_subject_id_createdat ON "Subject" (id, "createdAt");

CREATE INDEX idx_subject_subjectname ON "Subject" ("subjectName");

CREATE INDEX idx_chapter_subjectid ON "Chapter" ("subjectId");

CREATE INDEX idx_subject_createdat_subjectname_id ON "Subject" ("createdAt", "subjectName", id);


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