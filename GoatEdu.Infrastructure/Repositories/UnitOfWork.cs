using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ChapterInterfaces;
using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure.Data;
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


    public UnitOfWork(GoatEduContext context)
    {
        _context = context;
    }
    
    //add interface of repo
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
    public INotificationRepository NotificationRepository => _notificationRepository ?? new NotificationRepository(_context);
    public INoteRepository NoteRepository => _noteRepository ?? new NoteRepository(_context);
    public ISubjectRepository SubjectRepository => _subjectRepository ?? new SubjectRepository(_context);
    public IChapterRepository ChapterRepository => _chapterRepository ?? new ChapterRepository(_context);
    public ILessonRepository LessonRepository => _lessonRepository ?? new LessonRepository(_context);



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

}