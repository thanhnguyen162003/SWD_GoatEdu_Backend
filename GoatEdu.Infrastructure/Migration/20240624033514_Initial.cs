using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migration
{
    public partial class Initial : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Calculation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: true),
                    lessonCount = table.Column<int>(type: "integer", nullable: true),
                    chapterCount = table.Column<int>(type: "integer", nullable: true),
                    quizCount = table.Column<int>(type: "integer", nullable: true),
                    theoryCount = table.Column<int>(type: "integer", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    roleName = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    subjectName = table.Column<string>(type: "character varying", nullable: true),
                    subjectCode = table.Column<string>(type: "character varying", nullable: true),
                    information = table.Column<string>(type: "character varying", nullable: true),
                    @class = table.Column<string>(name: "class", type: "character varying", nullable: true),
                    image = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    subscriptionName = table.Column<string>(type: "character varying", nullable: true),
                    subscriptionBody = table.Column<string>(type: "character varying", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: true),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    tagName = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    numberWallet = table.Column<double>(type: "double precision", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    chapterName = table.Column<string>(type: "character varying", nullable: true),
                    chapterLevel = table.Column<int>(type: "integer", nullable: true),
                    subjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.id);
                    table.ForeignKey(
                        name: "FK_Chapter_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    transactionName = table.Column<string>(type: "character varying", nullable: true),
                    note = table.Column<string>(type: "character varying", nullable: true),
                    walletId = table.Column<Guid>(type: "uuid", nullable: true),
                    startDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    endDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    subscriptionId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transaction_Subscription_subscriptionId",
                        column: x => x.subscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    username = table.Column<string>(type: "character varying", nullable: true),
                    fullname = table.Column<string>(type: "character varying", nullable: true),
                    image = table.Column<string>(type: "character varying", nullable: true),
                    password = table.Column<string>(type: "character varying", nullable: true),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    phoneNumber = table.Column<string>(type: "character varying", nullable: true),
                    subscription = table.Column<string>(type: "character varying", nullable: true),
                    subscriptionEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    provider = table.Column<string>(type: "character varying", nullable: true),
                    emailVerify = table.Column<bool>(type: "boolean", nullable: true),
                    roleId = table.Column<Guid>(type: "uuid", nullable: true),
                    walletId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isNewUser = table.Column<bool>(type: "boolean", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role_roleId",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_User_Wallet_walletId",
                        column: x => x.walletId,
                        principalTable: "Wallet",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    lessonName = table.Column<string>(type: "character varying", nullable: true),
                    lessonBody = table.Column<string>(type: "character varying", nullable: true),
                    lessonMaterial = table.Column<string>(type: "character varying", nullable: true),
                    chapterId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    displayOrder = table.Column<int>(type: "integer", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lesson_Chapter_chapterId",
                        column: x => x.chapterId,
                        principalTable: "Chapter",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    achievementName = table.Column<string>(type: "character varying", nullable: true),
                    achievementContent = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.id);
                    table.ForeignKey(
                        name: "FK_Achievement_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Discussion",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    discussionName = table.Column<string>(type: "character varying", nullable: true),
                    discussionBody = table.Column<string>(type: "character varying", nullable: true),
                    discussionBodyHtml = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    discussionImage = table.Column<string>(type: "character varying", nullable: true),
                    discussionVote = table.Column<int>(type: "integer", nullable: true),
                    subjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    isSolved = table.Column<bool>(type: "boolean", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Discussion_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Discussion_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    subjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Enrollment_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Enrollment_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Flashcard",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    flashcardName = table.Column<string>(type: "character varying", nullable: true),
                    flashcardDescription = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    subjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    star = table.Column<int>(type: "integer", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flashcard", x => x.id);
                    table.ForeignKey(
                        name: "FK_Flashcard_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Flashcard_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    noteName = table.Column<string>(type: "character varying", nullable: true),
                    noteBody = table.Column<string>(type: "character varying", nullable: true),
                    noteBodyHtml = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.id);
                    table.ForeignKey(
                        name: "FK_Note_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    notificationName = table.Column<string>(type: "character varying", nullable: true),
                    notificationMessage = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    readAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notification_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    reportTitle = table.Column<string>(type: "character varying", nullable: true),
                    reportContent = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.id);
                    table.ForeignKey(
                        name: "FK_Report_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    quiz = table.Column<string>(type: "character varying", nullable: true),
                    quizLevel = table.Column<int>(type: "integer", nullable: true),
                    lessonId = table.Column<Guid>(type: "uuid", nullable: true),
                    chapterId = table.Column<Guid>(type: "uuid", nullable: true),
                    subjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isRequire = table.Column<bool>(type: "boolean", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.id);
                    table.ForeignKey(
                        name: "FK_Quiz_Chapter_chapterId",
                        column: x => x.chapterId,
                        principalTable: "Chapter",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Quiz_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Quiz_Subject_subjectId",
                        column: x => x.subjectId,
                        principalTable: "Subject",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Theory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    theoryName = table.Column<string>(type: "character varying", nullable: true),
                    file = table.Column<string>(type: "character varying", nullable: true),
                    image = table.Column<string>(type: "character varying", nullable: true),
                    theoryContent = table.Column<string>(type: "character varying", nullable: true),
                    lessonId = table.Column<Guid>(type: "uuid", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theory", x => x.id);
                    table.ForeignKey(
                        name: "FK_Theory_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    answerName = table.Column<string>(type: "character varying", nullable: true),
                    answerBody = table.Column<string>(type: "character varying", nullable: true),
                    answerBodyHtml = table.Column<string>(type: "character varying", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    questionId = table.Column<Guid>(type: "uuid", nullable: true),
                    answerImage = table.Column<string>(type: "character varying", nullable: true),
                    answerVote = table.Column<int>(type: "integer", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_Answer_Discussion_questionId",
                        column: x => x.questionId,
                        principalTable: "Discussion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Answer_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DiscussionTag",
                columns: table => new
                {
                    DiscussionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionTag", x => new { x.DiscussionsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DiscussionTag_Discussion_DiscussionsId",
                        column: x => x.DiscussionsId,
                        principalTable: "Discussion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentProcess",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    enrollmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    process = table.Column<int>(type: "integer", nullable: true),
                    chapterId = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentProcess", x => x.id);
                    table.ForeignKey(
                        name: "FK_EnrollmentProcess_Enrollment_enrollmentId",
                        column: x => x.enrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FlashcardContent",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    flashcardContentQuestion = table.Column<string>(type: "character varying", nullable: true),
                    flashcardContentAnswer = table.Column<string>(type: "character varying", nullable: true),
                    flashcardId = table.Column<Guid>(type: "uuid", nullable: true),
                    image = table.Column<string>(type: "character varying", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedBy = table.Column<string>(type: "character varying", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardContent", x => x.id);
                    table.ForeignKey(
                        name: "FK_FlashcardContent_Flashcard_flashcardId",
                        column: x => x.flashcardId,
                        principalTable: "Flashcard",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FlashcardTag",
                columns: table => new
                {
                    FlashcardsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashcardTag", x => new { x.FlashcardsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FlashcardTag_Flashcard_FlashcardsId",
                        column: x => x.FlashcardsId,
                        principalTable: "Flashcard",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlashcardTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flashcardId = table.Column<Guid>(type: "uuid", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    rateValue = table.Column<short>(type: "smallint", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rate_Flashcard_flashcardId",
                        column: x => x.flashcardId,
                        principalTable: "Flashcard",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Rate_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionInQuiz",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    quizId = table.Column<Guid>(type: "uuid", nullable: true),
                    quizQuestion = table.Column<string>(type: "character varying", nullable: true),
                    quizAnswer1 = table.Column<string>(type: "character varying", nullable: true),
                    quizAnswer2 = table.Column<string>(type: "character varying", nullable: true),
                    quizAnswer3 = table.Column<string>(type: "character varying", nullable: true),
                    quizCorrect = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionInQuiz", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuestionInQuiz_Quiz_quizId",
                        column: x => x.quizId,
                        principalTable: "Quiz",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TheoryFlashCardContent",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    question = table.Column<string>(type: "character varying", nullable: true),
                    answer = table.Column<string>(type: "character varying", nullable: true),
                    theoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<string>(type: "character varying", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoryFlashCardContent", x => x.id);
                    table.ForeignKey(
                        name: "FK_TheoryFlashCardContent_Theory_theoryId",
                        column: x => x.theoryId,
                        principalTable: "Theory",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discussionId = table.Column<Guid>(type: "uuid", nullable: true),
                    answerId = table.Column<Guid>(type: "uuid", nullable: true),
                    userId = table.Column<Guid>(type: "uuid", nullable: true),
                    voteValue = table.Column<short>(type: "smallint", nullable: true),
                    voteTimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Votes_Answer_answerId",
                        column: x => x.answerId,
                        principalTable: "Answer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Votes_Discussion_discussionId",
                        column: x => x.discussionId,
                        principalTable: "Discussion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Votes_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_userId",
                table: "Achievement",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_questionId",
                table: "Answer",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_userId",
                table: "Answer",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "idx_chapter_subjectid_isdeleted",
                table: "Chapter",
                columns: new[] { "subjectId", "isDeleted" });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_subjectId",
                table: "Chapter",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_subjectId",
                table: "Discussion",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussion_userId",
                table: "Discussion",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionTag_TagsId",
                table: "DiscussionTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "Enrollment_userId_subjectId_key",
                table: "Enrollment",
                columns: new[] { "userId", "subjectId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_subjectId",
                table: "Enrollment",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "EnrollmentProcess_enrollmentId_key",
                table: "EnrollmentProcess",
                column: "enrollmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flashcard_subjectId",
                table: "Flashcard",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Flashcard_userId",
                table: "Flashcard",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardContent_flashcardId",
                table: "FlashcardContent",
                column: "flashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_FlashcardTag_TagsId",
                table: "FlashcardTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_chapterId",
                table: "Lesson",
                column: "chapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_userId",
                table: "Note",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_userId",
                table: "Notification",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionInQuiz_quizId",
                table: "QuestionInQuiz",
                column: "quizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_chapterId",
                table: "Quiz",
                column: "chapterId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_lessonId",
                table: "Quiz",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_subjectId",
                table: "Quiz",
                column: "subjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_flashcardId",
                table: "Rate",
                column: "flashcardId");

            migrationBuilder.CreateIndex(
                name: "IX_Rate_userId",
                table: "Rate",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_userId",
                table: "Report",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "idx_subject_isdeleted",
                table: "Subject",
                column: "isDeleted");

            migrationBuilder.CreateIndex(
                name: "idx_subject_ordering",
                table: "Subject",
                columns: new[] { "createdAt", "subjectName", "id" });

            migrationBuilder.CreateIndex(
                name: "IX_Theory_lessonId",
                table: "Theory",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_TheoryFlashCardContent_theoryId",
                table: "TheoryFlashCardContent",
                column: "theoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_subscriptionId",
                table: "Transaction",
                column: "subscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roleId",
                table: "User",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_walletId",
                table: "User",
                column: "walletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_answerId",
                table: "Votes",
                column: "answerId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_discussionId",
                table: "Votes",
                column: "discussionId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_userId",
                table: "Votes",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "Calculation");

            migrationBuilder.DropTable(
                name: "DiscussionTag");

            migrationBuilder.DropTable(
                name: "EnrollmentProcess");

            migrationBuilder.DropTable(
                name: "FlashcardContent");

            migrationBuilder.DropTable(
                name: "FlashcardTag");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "QuestionInQuiz");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "TheoryFlashCardContent");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Flashcard");

            migrationBuilder.DropTable(
                name: "Theory");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Discussion");

            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Wallet");
        }
    }
}
