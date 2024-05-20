using Infrastructure;

namespace GoatEdu.Core.Interfaces.GenericInterfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(Guid? id);
    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    void Update(T entity);
    void UpdateRange(List<T> entities);

}
