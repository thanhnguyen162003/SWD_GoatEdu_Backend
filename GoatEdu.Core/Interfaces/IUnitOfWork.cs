using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;

namespace GoatEdu.Core.Interfaces
{

    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        
        void SaveChanges();
        Task SaveChangesAsync();
    }
}