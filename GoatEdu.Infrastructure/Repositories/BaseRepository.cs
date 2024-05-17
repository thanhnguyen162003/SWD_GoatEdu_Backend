using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly GoatEduContext _context;
    protected readonly DbSet<T> _entities;
    public BaseRepository(GoatEduContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public T GetById(Guid? id)
    {
        return _entities.Find(id);
    }

    public void Add(T entity)
    {
        entity.IsDeleted = false;
        _entities.AddAsync(entity);
    }
    
    public IEnumerable<T> GetAll()
    {
        return _entities.Where(x => x.IsDeleted == false).AsEnumerable().ToList();
    }
    public void Update(T entity)
    {
        _entities.Update(entity);
    }
}
