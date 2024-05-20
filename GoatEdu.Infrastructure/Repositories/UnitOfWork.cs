using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.NoteInterfaces;
using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
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

    public UnitOfWork(GoatEduContext context)
    {
        _context = context;
    }
    
    //add interface of repo
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_context);
    public INotificationRepository NotificationRepository => _notificationRepository ?? new NotificationRepository(_context);
    public INoteRepository NoteRepository => _noteRepository ?? new NoteRepository(_context);

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