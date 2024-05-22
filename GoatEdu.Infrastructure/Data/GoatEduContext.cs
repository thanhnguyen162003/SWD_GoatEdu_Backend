using System;
using System.Collections.Generic;
using GoatEdu.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public partial class GoatEduContext : DbContext
    {
        public GoatEduContext()
        {
        }

        public GoatEduContext(DbContextOptions<GoatEduContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Achievement> Achievements { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Calculation> Calculations { get; set; } = null!;
        public virtual DbSet<Chapter> Chapters { get; set; } = null!;
        public virtual DbSet<Discussion> Discussions { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<EnrollmentProcess> EnrollmentProcesses { get; set; } = null!;
        public virtual DbSet<Flashcard> Flashcards { get; set; } = null!;
        public virtual DbSet<FlashcardContent> FlashcardContents { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<QuestionInQuiz> QuestionInQuizzes { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Theory> Theories { get; set; } = null!;
        public virtual DbSet<TheoryFlashCardContent> TheoryFlashCardContents { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Vote> Votes { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=35.240.220.220;Port=5432;Username=root;Password=Admin123456789@;Database=goateduprimary;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.ToTable("Achievement");

                entity.HasIndex(e => e.UserId, "IX_Achievement_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AchievementContent)
                    .HasColumnType("character varying")
                    .HasColumnName("achievementContent");

                entity.Property(e => e.AchievementName)
                    .HasColumnType("character varying")
                    .HasColumnName("achievementName");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Achievement_userId_fkey");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.HasIndex(e => e.QuestionId, "IX_Answer_questionId");

                entity.HasIndex(e => e.UserId, "IX_Answer_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AnswerBody)
                    .HasColumnType("character varying")
                    .HasColumnName("answerBody");

                entity.Property(e => e.AnswerImage)
                    .HasColumnType("character varying")
                    .HasColumnName("answerImage");

                entity.Property(e => e.AnswerName)
                    .HasColumnType("character varying")
                    .HasColumnName("answerName");

                entity.Property(e => e.AnswerVote).HasColumnName("answerVote");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updatedBy");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("Answer_questionId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Answer_userId_fkey");
            });

            modelBuilder.Entity<Calculation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Calculation");

                entity.Property(e => e.ChapterCount).HasColumnName("chapterCount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LessonCount).HasColumnName("lessonCount");

                entity.Property(e => e.QuizCount).HasColumnName("quizCount");

                entity.Property(e => e.TheoryCount).HasColumnName("theoryCount");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.ToTable("Chapter");

                entity.HasIndex(e => e.SubjectId, "IX_Chapter_subjectId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ChapterLevel).HasColumnName("chapterLevel");

                entity.Property(e => e.ChapterName)
                    .HasColumnType("character varying")
                    .HasColumnName("chapterName");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Chapter_subjectId_fkey");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.ToTable("Discussion");

                entity.HasIndex(e => e.SubjectId, "IX_Discussion_subjectId");

                entity.HasIndex(e => e.UserId, "IX_Discussion_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.DiscussionBody)
                    .HasColumnType("character varying")
                    .HasColumnName("discussionBody");

                entity.Property(e => e.DiscussionImage)
                    .HasColumnType("character varying")
                    .HasColumnName("discussionImage");

                entity.Property(e => e.DiscussionName)
                    .HasColumnType("character varying")
                    .HasColumnName("discussionName");

                entity.Property(e => e.DiscussionVote).HasColumnName("discussionVote");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsSolved).HasColumnName("isSolved");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updatedBy");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Discussion_subjectId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Discussion_userId_fkey");

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Discussions)
                    .UsingEntity<Dictionary<string, object>>(
                        "DiscussionTag",
                        l => l.HasOne<Tag>().WithMany().HasForeignKey("TagsId"),
                        r => r.HasOne<Discussion>().WithMany().HasForeignKey("DiscussionsId"),
                        j =>
                        {
                            j.HasKey("DiscussionsId", "TagsId");

                            j.ToTable("DiscussionTag");

                            j.HasIndex(new[] { "TagsId" }, "IX_DiscussionTag_TagsId");
                        });
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.HasIndex(e => new { e.UserId, e.SubjectId }, "Enrollment_userId_subjectId_key")
                    .IsUnique();

                entity.HasIndex(e => e.SubjectId, "IX_Enrollment_subjectId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Enrollment_subjectId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Enrollment_userId_fkey");
            });

            modelBuilder.Entity<EnrollmentProcess>(entity =>
            {
                entity.ToTable("EnrollmentProcess");

                entity.HasIndex(e => e.EnrollmentId, "EnrollmentProcess_enrollmentId_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ChapterId).HasColumnName("chapterId");

                entity.Property(e => e.EnrollmentId).HasColumnName("enrollmentId");

                entity.Property(e => e.Process).HasColumnName("process");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.HasOne(d => d.Enrollment)
                    .WithOne(p => p.EnrollmentProcess)
                    .HasForeignKey<EnrollmentProcess>(d => d.EnrollmentId)
                    .HasConstraintName("EnrollmentProcess_enrollmentId_fkey");
            });

            modelBuilder.Entity<Flashcard>(entity =>
            {
                entity.ToTable("Flashcard");

                entity.HasIndex(e => e.SubjectId, "IX_Flashcard_subjectId");

                entity.HasIndex(e => e.UserId, "IX_Flashcard_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.FlashcardDescription)
                    .HasColumnType("character varying")
                    .HasColumnName("flashcardDescription");

                entity.Property(e => e.FlashcardName)
                    .HasColumnType("character varying")
                    .HasColumnName("flashcardName");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Star).HasColumnName("star");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.TagId).HasColumnName("tagId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updatedBy");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Flashcards)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Flashcard_subjectId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Flashcards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Flashcard_userId_fkey");

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.Flashcards)
                    .UsingEntity<Dictionary<string, object>>(
                        "FlashcardTag",
                        l => l.HasOne<Tag>().WithMany().HasForeignKey("TagsId"),
                        r => r.HasOne<Flashcard>().WithMany().HasForeignKey("FlashcardsId"),
                        j =>
                        {
                            j.HasKey("FlashcardsId", "TagsId");

                            j.ToTable("FlashcardTag");

                            j.HasIndex(new[] { "TagsId" }, "IX_FlashcardTag_TagsId");
                        });
            });

            modelBuilder.Entity<FlashcardContent>(entity =>
            {
                entity.ToTable("FlashcardContent");

                entity.HasIndex(e => e.FlashcardId, "IX_FlashcardContent_flashcardId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.FlashcardContentAnswer)
                    .HasColumnType("character varying")
                    .HasColumnName("flashcardContentAnswer");

                entity.Property(e => e.FlashcardContentQuestion)
                    .HasColumnType("character varying")
                    .HasColumnName("flashcardContentQuestion");

                entity.Property(e => e.FlashcardId).HasColumnName("flashcardId");

                entity.Property(e => e.Image)
                    .HasColumnType("character varying")
                    .HasColumnName("image");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updatedBy");

                entity.HasOne(d => d.Flashcard)
                    .WithMany(p => p.FlashcardContents)
                    .HasForeignKey(d => d.FlashcardId)
                    .HasConstraintName("FlashcardContent_flashcardId_fkey");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.HasIndex(e => e.ChapterId, "IX_Lesson_chapterId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ChapterId).HasColumnName("chapterId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.DisplayOrder).HasColumnName("displayOrder");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LessonBody)
                    .HasColumnType("character varying")
                    .HasColumnName("lessonBody");

                entity.Property(e => e.LessonMaterial)
                    .HasColumnType("character varying")
                    .HasColumnName("lessonMaterial");

                entity.Property(e => e.LessonName)
                    .HasColumnType("character varying")
                    .HasColumnName("lessonName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updatedBy");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("Lesson_chapterId_fkey");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Note");

                entity.HasIndex(e => e.UserId, "IX_Note_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.NoteBody)
                    .HasColumnType("character varying")
                    .HasColumnName("noteBody");

                entity.Property(e => e.NoteName)
                    .HasColumnType("character varying")
                    .HasColumnName("noteName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("updatedBy");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Note_userId_fkey");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.HasIndex(e => e.UserId, "IX_Notification_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.NotificationMessage)
                    .HasColumnType("character varying")
                    .HasColumnName("notificationMessage");

                entity.Property(e => e.NotificationName)
                    .HasColumnType("character varying")
                    .HasColumnName("notificationName");

                entity.Property(e => e.ReadAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("readAt");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Notification_userId_fkey");
            });

            modelBuilder.Entity<QuestionInQuiz>(entity =>
            {
                entity.ToTable("QuestionInQuiz");

                entity.HasIndex(e => e.QuizId, "IX_QuestionInQuiz_quizId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.QuizAnswer1)
                    .HasColumnType("character varying")
                    .HasColumnName("quizAnswer1");

                entity.Property(e => e.QuizAnswer2)
                    .HasColumnType("character varying")
                    .HasColumnName("quizAnswer2");

                entity.Property(e => e.QuizAnswer3)
                    .HasColumnType("character varying")
                    .HasColumnName("quizAnswer3");

                entity.Property(e => e.QuizCorrect)
                    .HasColumnType("character varying")
                    .HasColumnName("quizCorrect");

                entity.Property(e => e.QuizId).HasColumnName("quizId");

                entity.Property(e => e.QuizQuestion)
                    .HasColumnType("character varying")
                    .HasColumnName("quizQuestion");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuestionInQuizzes)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("QuestionInQuiz_quizId_fkey");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.HasIndex(e => e.ChapterId, "IX_Quiz_chapterId");

                entity.HasIndex(e => e.LessonId, "IX_Quiz_lessonId");

                entity.HasIndex(e => e.SubjectId, "IX_Quiz_subjectId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ChapterId).HasColumnName("chapterId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsRequire).HasColumnName("isRequire");

                entity.Property(e => e.LessonId).HasColumnName("lessonId");

                entity.Property(e => e.Quiz1)
                    .HasColumnType("character varying")
                    .HasColumnName("quiz");

                entity.Property(e => e.QuizLevel).HasColumnName("quizLevel");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("Quiz_chapterId_fkey");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("Quiz_lessonId_fkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Quiz_subjectId_fkey");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.HasIndex(e => e.UserId, "IX_Report_userId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("character varying")
                    .HasColumnName("createdBy");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.ReportContent)
                    .HasColumnType("character varying")
                    .HasColumnName("reportContent");

                entity.Property(e => e.ReportTitle)
                    .HasColumnType("character varying")
                    .HasColumnName("reportTitle");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Report_userId_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.RoleName)
                    .HasColumnType("character varying")
                    .HasColumnName("roleName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Class)
                    .HasColumnType("character varying")
                    .HasColumnName("class");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Image)
                    .HasColumnType("character varying")
                    .HasColumnName("image");

                entity.Property(e => e.Information)
                    .HasColumnType("character varying")
                    .HasColumnName("information");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.SubjectCode)
                    .HasColumnType("character varying")
                    .HasColumnName("subjectCode");

                entity.Property(e => e.SubjectName)
                    .HasColumnType("character varying")
                    .HasColumnName("subjectName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscription");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SubscriptionBody)
                    .HasColumnType("character varying")
                    .HasColumnName("subscriptionBody");

                entity.Property(e => e.SubscriptionName)
                    .HasColumnType("character varying")
                    .HasColumnName("subscriptionName");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.TagName)
                    .HasColumnType("character varying")
                    .HasColumnName("tagName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Theory>(entity =>
            {
                entity.ToTable("Theory");

                entity.HasIndex(e => e.LessonId, "IX_Theory_lessonId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.File)
                    .HasColumnType("character varying")
                    .HasColumnName("file");

                entity.Property(e => e.Image)
                    .HasColumnType("character varying")
                    .HasColumnName("image");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.LessonId).HasColumnName("lessonId");

                entity.Property(e => e.TheoryContent)
                    .HasColumnType("character varying")
                    .HasColumnName("theoryContent");

                entity.Property(e => e.TheoryName)
                    .HasColumnType("character varying")
                    .HasColumnName("theoryName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Theories)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("Theory_lessonId_fkey");
            });

            modelBuilder.Entity<TheoryFlashCardContent>(entity =>
            {
                entity.ToTable("TheoryFlashCardContent");

                entity.HasIndex(e => e.TheoryId, "IX_TheoryFlashCardContent_theoryId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Answer)
                    .HasColumnType("character varying")
                    .HasColumnName("answer");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Question)
                    .HasColumnType("character varying")
                    .HasColumnName("question");

                entity.Property(e => e.Status)
                    .HasColumnType("character varying")
                    .HasColumnName("status");

                entity.Property(e => e.TheoryId).HasColumnName("theoryId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Theory)
                    .WithMany(p => p.TheoryFlashCardContents)
                    .HasForeignKey(d => d.TheoryId)
                    .HasConstraintName("TheoryFlashCardContent_theoryId_fkey");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.HasIndex(e => e.SubscriptionId, "IX_Transaction_subscriptionId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.EndDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("endDate");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Note)
                    .HasColumnType("character varying")
                    .HasColumnName("note");

                entity.Property(e => e.StartDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("startDate");

                entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");

                entity.Property(e => e.TransactionName)
                    .HasColumnType("character varying")
                    .HasColumnName("transactionName");

                entity.Property(e => e.WalletId).HasColumnName("walletId");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("Transaction_subscriptionId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.RoleId, "IX_User_roleId");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Email)
                    .HasColumnType("character varying")
                    .HasColumnName("email");

                entity.Property(e => e.EmailVerify).HasColumnName("emailVerify");

                entity.Property(e => e.Fullname)
                    .HasColumnType("character varying")
                    .HasColumnName("fullname");

                entity.Property(e => e.Image)
                    .HasColumnType("character varying")
                    .HasColumnName("image");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Password)
                    .HasColumnType("character varying")
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("character varying")
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Provider)
                    .HasColumnType("character varying")
                    .HasColumnName("provider");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Subscription)
                    .HasColumnType("character varying")
                    .HasColumnName("subscription");

                entity.Property(e => e.SubscriptionEnd)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("subscriptionEnd");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.Property(e => e.Username)
                    .HasColumnType("character varying")
                    .HasColumnName("username");

                entity.Property(e => e.WalletId).HasColumnName("walletId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("User_roleId_fkey");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.ToTable("Vote");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('votes_voteid_seq'::regclass)");

                entity.Property(e => e.Answerid).HasColumnName("answerid");

                entity.Property(e => e.Discussionid).HasColumnName("discussionid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Votetimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("votetimestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Votevalue).HasColumnName("votevalue");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Answerid)
                    .HasConstraintName("vote_answerid_fkey");

                entity.HasOne(d => d.Discussion)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Discussionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vote_discussionid_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vote_userid_fkey");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.NumberWallet).HasColumnName("numberWallet");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Wallet)
                    .HasForeignKey<Wallet>(d => d.Id)
                    .HasConstraintName("Wallet_userId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
