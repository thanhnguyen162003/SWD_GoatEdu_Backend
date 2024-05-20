using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly GoatEduContext _context;
    protected readonly DbSet<T> _entities;
    public BaseRepository(GoatEduContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid? id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        // entity.IsDeleted = false;
        await _entities.AddAsync(entity);
    }

    public async Task AddRangeAsync(List<T> entities)
    {
        await _entities.AddRangeAsync(entities);
    }
    
    public IQueryable<T> GetAll()
    {
        return _entities.AsQueryable();
    }
    public void Update(T entity)
    {
        _entities.Update(entity);
    }

    public void UpdateRange(List<T> entities)
    {
        _entities.UpdateRange(entities);
    }
}
