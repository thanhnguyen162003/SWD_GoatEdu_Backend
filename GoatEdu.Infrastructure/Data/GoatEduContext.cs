using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastructure.Models;

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
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Calculation> Calculations { get; set; } = null!;
        public virtual DbSet<Chapter> Chapters { get; set; } = null!;
        public virtual DbSet<Discussion> Discussions { get; set; } = null!;
        public virtual DbSet<Flashcard> Flashcards { get; set; } = null!;
        public virtual DbSet<FlashcardContent> FlashcardContents { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Moderator> Moderators { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<Theory> Theories { get; set; } = null!;
        public virtual DbSet<TheoryFlashcardContent> TheoryFlashcardContents { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSubject> UserSubjects { get; set; } = null!;
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
                entity.Property(e => e.AchievementId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("achievements_user_id_fkey");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.AnswerId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("answers_question_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("answers_user_id_fkey");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(e => e.ChapterId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("chapters_subject_id_fkey");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.DiscussionId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("discussions_subject_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("discussions_user_id_fkey");
            });

            modelBuilder.Entity<Flashcard>(entity =>
            {
                entity.Property(e => e.FlashcardId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Flashcards)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("flashcards_subject_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Flashcards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("flashcards_user_id_fkey");
            });

            modelBuilder.Entity<FlashcardContent>(entity =>
            {
                entity.Property(e => e.FlashcardContentId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Flashcard)
                    .WithMany(p => p.FlashcardContents)
                    .HasForeignKey(d => d.FlashcardId)
                    .HasConstraintName("flashcard_contents_flashcard_id_fkey");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.LessonId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("lessons_chapter_id_fkey");
            });

            modelBuilder.Entity<Moderator>(entity =>
            {
                entity.Property(e => e.ModeratorId).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.NoteId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("notes_user_id_fkey");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("notifications_user_id_fkey");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.QuizId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("quizzes_lesson_id_fkey");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.ReportId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("reports_user_id_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.SubscriptionId).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Theory>(entity =>
            {
                entity.Property(e => e.TheoryId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Theories)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("theories_lesson_id_fkey");
            });

            modelBuilder.Entity<TheoryFlashcardContent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Theory)
                    .WithMany(p => p.TheoryFlashcardContents)
                    .HasForeignKey(d => d.TheoryId)
                    .HasConstraintName("theory_flashcard_content_theory_id_fkey");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("transactions_subscription_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("users_role_id_fkey");
            });

            modelBuilder.Entity<UserSubject>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SubjectId })
                    .HasName("user_subject_pkey");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.UserSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_subject_subject_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSubjects)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_subject_user_id_fkey");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.WalletId).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("wallets_transaction_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("wallets_user_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
