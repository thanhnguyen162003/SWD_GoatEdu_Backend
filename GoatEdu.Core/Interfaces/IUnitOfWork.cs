using GoatEdu.Core.Interfaces.AdminInterfaces;
using GoatEdu.Core.Interfaces.AnswerInterfaces;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.Interfaces.DiscussionInterfaces;
using GoatEdu.Core.Interfaces.EnrollmentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardContentInterfaces;
using GoatEdu.Core.Interfaces.FlashcardInterfaces;
using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.RateInterfaces;
using GoatEdu.Core.Interfaces.ReportInterfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.Interfaces.TagInterfaces;
using GoatEdu.Core.Interfaces.TranstractionInterfaces;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Interfaces.VoteInterface;
using GoatEdu.Core.Interfaces.WalletInterfaces;

namespace GoatEdu.Core.Interfaces
{

    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        INotificationRepository NotificationRepository { get; }
        INoteRepository NoteRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IChapterRepository ChapterRepository { get; }
        ILessonRepository LessonRepository { get; }
        ITagRepository TagRepository { get; }
        IFlashcardRepository FlashcardRepository { get; }
        IDiscussionRepository DiscussionRepository { get; }
        IUserDetailRepository UserDetailRepository { get; }
        IAdminRepository AdminRepository { get; }
        IReportRepository ReportRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IEnrollmentProcessRepository EnrollmentProcessRepository { get; }
        IModeratorRepository ModeratorRepository { get; }
        IFlashcardContentRepository FlashcardContentRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IRateRepository RateRepository { get; }
        IVoteRepository VoteRepository { get; }
        IWalletRepository WalletRepository { get; }
        ITranstractionRepository TranstractionRepository { get; }
        ISubcriptionRepository SubcriptionRepository { get; }



        
        
        void SaveChanges();
        Task<int> SaveChangesAsync();
        // Transaction error
        // Task BeginTransactionAsync();
        // Task CommitTransactionAsync();
        // Task RollbackTransactionAsync();
    }
}