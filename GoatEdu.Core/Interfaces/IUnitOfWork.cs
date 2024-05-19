using GoatEdu.Core.Interfaces.NotificationInterfaces;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.UserInterfaces;

namespace GoatEdu.Core.Interfaces
{

    public interface IUnitOfWork
    {
        
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        INotificationRepository NotificationRepository { get; }
        
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}