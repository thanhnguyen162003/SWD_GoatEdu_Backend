using Infrastructure.Models;

namespace GoatEdu.Core.Interfaces.GenericInterfaces;

public interface IRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    T GetById(int? id);
    void Add(T entity);
    void Update(T entity);
    
}
