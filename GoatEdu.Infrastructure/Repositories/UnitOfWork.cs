using GoatEdu.Core.Interfaces;
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
using GoatEdu.Core.Interfaces.TheoryInterfaces;
using GoatEdu.Core.Interfaces.TranstractionInterfaces;
using GoatEdu.Core.Interfaces.UserDetailInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Interfaces.VoteInterface;
using GoatEdu.Core.Interfaces.WalletInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories.CacheRepository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GoatEduContext _context;
    
    //add interface of repo
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly INoteRepository _noteRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IChapterRepository _chapterRepository;
    private readonly ILessonRepository _lessonRepository;
    private readonly IDistributedCache _distributedCache;
    private readonly ITagRepository _tagRepository;
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly IDiscussionRepository _discussionRepository;   
    private readonly IUserDetailRepository _userDetailRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IEnrollmentProcessRepository _enrollmentProcessRepository;
    private readonly IModeratorRepository _moderatorRepository;
    private readonly IFlashcardContentRepository _flashcardContentRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IRateRepository _rateRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly ITranstractionRepository _transtractionRepository;
    private readonly ISubcriptionRepository _subcriptionRepository;
    private readonly ITheoryRepository _theoryRepository;


    private IDbContextTransaction _transaction;


    public UnitOfWork(GoatEduContext context, IDistributedCache distributedCache)
    {
        _context = context;
        _distributedCache = distributedCache;
    }
    
    //add interface of repo
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    public IRoleRepository RoleRepository => _roleRepository ?? new CachedRoleRepository(new RoleRepository(_context), _distributedCache, _context);
    public INotificationRepository NotificationRepository => _notificationRepository ?? new NotificationRepository(_context);
    public INoteRepository NoteRepository => _noteRepository ?? new NoteRepository(_context);
    public ISubjectRepository SubjectRepository =>
        _subjectRepository ?? new CacheSubjectRepository(new SubjectRepository(_context), _distributedCache, _context);
    public IChapterRepository ChapterRepository => _chapterRepository ?? new ChapterRepository(_context);
    public ILessonRepository LessonRepository => _lessonRepository ?? new LessonRepository(_context);
    public ITagRepository TagRepository => _tagRepository ?? new TagRepository(_context);
    public IFlashcardRepository FlashcardRepository => _flashcardRepository ?? new FlashcardRepository(_context);
    public IDiscussionRepository DiscussionRepository => _discussionRepository ?? new DiscussionRepository(_context);
    public IUserDetailRepository UserDetailRepository => _userDetailRepository ?? new UserDetailRepository(_context);
    public IAdminRepository AdminRepository => _adminRepository ?? new AdminRepository(_context);
    public IReportRepository ReportRepository => _reportRepository ?? new ReportRepository(_context);
    public IEnrollmentRepository EnrollmentRepository => _enrollmentRepository ?? new EnrollmentRepository(_context);
    public IEnrollmentProcessRepository EnrollmentProcessRepository => _enrollmentProcessRepository ?? new EnrollmentProcessRepository(_context);
    public IModeratorRepository ModeratorRepository => _moderatorRepository ?? new ModeratorRepository(_context);
    public IFlashcardContentRepository FlashcardContentRepository => _flashcardContentRepository ?? new FlashcardContentRepository(_context);
    public IAnswerRepository AnswerRepository => _answerRepository ?? new AnswerRepository(_context);
    public IRateRepository RateRepository => _rateRepository ?? new RateRepository(_context);
    public IVoteRepository VoteRepository => _voteRepository ?? new VoteRepository(_context);
    public IWalletRepository WalletRepository => _walletRepository ?? new WalletRepository(_context);
    public ISubcriptionRepository SubcriptionRepository => _subcriptionRepository ?? new SubscriptionRepository(_context);
    public ITheoryRepository TheoryRepository => _theoryRepository ?? new TheoryRepository(_context);

    public ITranstractionRepository TranstractionRepository => _transtractionRepository ?? new TranstractionRepository(_context);



    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }
    
    // Testing Transaction
    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            return;
        }
    
        _transaction = await _context.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }
    
    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        finally
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
    
    private bool IsRedisAvailable()
    {
        try
        {
            _distributedCache.SetString("RedisConnectionTest", "test");
            var test = _distributedCache.GetString("RedisConnectionTest");
            return test == "test";
        }
        catch (Exception)
        {
            return false;
        }
    }
    

}