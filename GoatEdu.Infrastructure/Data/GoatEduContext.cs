using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastructure;

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
        public virtual DbSet<Theory> Theories { get; set; } = null!;
        public virtual DbSet<TheoryFlashCardContent> TheoryFlashCardContents { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
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
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Achievement_userId_fkey");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("Answer_questionId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Answer_userId_fkey");
            });

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Chapters)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Chapter_subjectId_fkey");
            });

            modelBuilder.Entity<Discussion>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Discussion_subjectId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Discussions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Discussion_userId_fkey");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

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
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Enrollment)
                    .WithOne(p => p.EnrollmentProcess)
                    .HasForeignKey<EnrollmentProcess>(d => d.EnrollmentId)
                    .HasConstraintName("EnrollmentProcess_enrollmentId_fkey");
            });

            modelBuilder.Entity<Flashcard>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Flashcards)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("Flashcard_subjectId_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Flashcards)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Flashcard_userId_fkey");
            });

            modelBuilder.Entity<FlashcardContent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Flashcard)
                    .WithMany(p => p.FlashcardContents)
                    .HasForeignKey(d => d.FlashcardId)
                    .HasConstraintName("FlashcardContent_flashcardId_fkey");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("Lesson_chapterId_fkey");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Note_userId_fkey");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Notification_userId_fkey");
            });

            modelBuilder.Entity<QuestionInQuiz>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuestionInQuizzes)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("QuestionInQuiz_quizId_fkey");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

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
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Report_userId_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            });

            modelBuilder.Entity<Theory>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Theories)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("Theory_lessonId_fkey");
            });

            modelBuilder.Entity<TheoryFlashCardContent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Theory)
                    .WithMany(p => p.TheoryFlashCardContents)
                    .HasForeignKey(d => d.TheoryId)
                    .HasConstraintName("TheoryFlashCardContent_theoryId_fkey");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("Transaction_subscriptionId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.Wallet)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.WalletId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("User_walletId_fkey");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("User_roleId_fkey");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Wallet)
                    .HasForeignKey<Wallet>(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Wallet_userId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
