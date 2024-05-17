namespace GoatEdu.Core.Interfaces
{

    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}