using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Infrastructure.Data;
namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GoatEduContext _context;
    
    //add interface of repo
    private readonly IUserRepository _userRepository;

    public UnitOfWork(GoatEduContext context)
    {
        _context = context;
    }
    
    //add interface of repo
    public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }

}