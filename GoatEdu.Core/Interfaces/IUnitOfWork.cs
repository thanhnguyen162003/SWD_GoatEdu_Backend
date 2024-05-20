using GoatEdu.Core.Interfaces.NoteInterfaces;
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
        INoteRepository NoteRepository { get; }
        
        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}