using GoatEdu.Core.Interfaces;
using Infrastructure.Data;
namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GoatEduContext _context;

    public UnitOfWork(GoatEduContext context)
    {
        _context = context;
    }
    
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}