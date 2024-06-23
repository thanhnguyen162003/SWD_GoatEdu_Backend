using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.DTOs.QuestionInQuizDto;
using GoatEdu.Core.DTOs.QuizDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.AdminInterfaces;
using GoatEdu.Core.Interfaces.AnswerInterfaces;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.CloudinaryInterfaces;
using GoatEdu.Core.Interfaces.DiscussionInterfaces;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.PaymentIntefaces;
using GoatEdu.Core.Interfaces.QuestionQuizInterfaces;
using GoatEdu.Core.Interfaces.QuizInterfaces;
using GoatEdu.Core.Interfaces.RateInterfaces;
using GoatEdu.Core.Interfaces.ReportInterfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.StripeInterface;
using GoatEdu.Core.Security;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.Interfaces.TagInterfaces;
using GoatEdu.Core.Interfaces.TheoryFlashcardContentInterfaces;
using GoatEdu.Core.Interfaces.TheoryInterfaces;
using GoatEdu.Core.Interfaces.TranstractionInterfaces;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Interfaces.VoteInterface;
using GoatEdu.Core.Interfaces.WalletInterfaces;
using GoatEdu.Core.Mappings;
using GoatEdu.Core.QueriesFilter;
using GoatEdu.Core.Services;
using GoatEdu.Core.Services.BackgroudTask;
using GoatEdu.Core.Validator;
using Infrastructure.Repositories;
using IMailService = GoatEdu.Core.Interfaces.MailInterfaces.IMailService;
using MailService = GoatEdu.Core.Services.MailService;

namespace GoatEdu.API;

public static class DI
{
    public static IServiceCollection AddWebApiService(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IChapterRepository, ChapterRepository>();
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IModeratorRepository, ModeratorRepository>();
        services.AddScoped<IFlashcardRepository, FlashcardRepository>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IFlashcardContentRepository, FlashcardContentRepository>();
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<IVoteRepository, VoteRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ISubcriptionRepository, SubscriptionRepository>();
        services.AddScoped<ITranstractionRepository, TranstractionRepository>();
        services.AddScoped<ITheoryRepository, TheoryRepository>();
        services.AddScoped<ITheoryFlashcardContentRepository, TheoryFlashcardContentRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuestionQuizRepository, QuestionQuizRepository>();


        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INoteService, NoteService>();        
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IChapterService, ChapterService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IDiscussionService, DiscussionService>();
        services.AddScoped<IUserDetailService, UserDetailService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IStripeService, StripeService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IModeratorService, ModeratorService>();
        services.AddScoped<IFlashcardService, FlashcardService>();
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<IFlashcardContentService, FlashcardContentService>();
        services.AddScoped<BackgroundTaskService>();
        services.AddSingleton<FlashcardHostedService>();
        services.AddSingleton<PeriodicHostedService>();
        services.AddScoped<IRateService, RateService>();
        services.AddScoped<IVoteService, VoteService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ITheoryService, TheoryService>();
        services.AddScoped<ITheoryFlashcardContentService, TheoryFlashcardContentService>();
        services.AddScoped<IQuizService, QuizService>();
        services.AddScoped<IQuestionQuizService, QuestionQuizService>();
        
        
        // Others
        services.AddAutoMapper(typeof(MapperConfigProfile).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<JWTGenerator, JWTConfig>();
        services.AddScoped<ICurrentTime, CurrentTime>();
        services.AddScoped<IClaimsService, ClaimsService>();
        services.AddHttpContextAccessor();
        services.AddScoped<IADProductService, ADProductService>();
        services.AddScoped<IBotAPIService, BotAPIService>();

        //Di for Google Cloud
        services.AddScoped<IGoogleCloudService, GoogleCloudService>();
        
        //DI for Cloudinary Cloud
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        
        // Add FluentValidation
        services.AddScoped<IValidator<SubjectDto>, SubjectDtoValidator>();
        services.AddScoped<IValidator<NotificationDto>, NotificationDtoValidator>();
        services.AddScoped<IValidator<NoteDto>, NoteDtoValidator>();
        services.AddScoped<IValidator<ChapterDto>, ChapterDtoValidator>();
        services.AddScoped<IValidator<TagDto>, TagDtoValidator>();
        services.AddScoped<IValidator<DiscussionDto>, DiscussionDtoValidator>();
        services.AddScoped<IValidator<AnswerDto>, AnswerDtoValidator>();
        services.AddScoped<IValidator<LessonDto>, LessonDtoValidator>();
        services.AddScoped<IValidator<TheoryDto>, TheoryDtoValidator>();
        services.AddScoped<IValidator<TheoryFlashcardContentsDto>, TheoryFlashcardContentDtoValidator>();
        services.AddScoped<IValidator<QuizDto>, QuizDtoValidator>();
        services.AddScoped<IValidator<QuizQueryFilter>, QuizQueryFilterValidator>();
        services.AddScoped<IValidator<QuestionInQuizDto>, QuestionQuizDtoValidator>();
        
        return services;
    }
}