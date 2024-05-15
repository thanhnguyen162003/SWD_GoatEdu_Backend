using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure.Models;

namespace GoatEdu.Core.Interfaces.UserInterfaces;

public interface IUserRepository : IRepository<User>
{
    IEnumerable<User> GetUserByName(string name);
    Task<User> GetUserByUsername(string username);
    Task<User> AddUser(User user);
}