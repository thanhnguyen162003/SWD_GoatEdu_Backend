using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.Models;
using Infrastructure;


namespace GoatEdu.Core.Interfaces.UserInterfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByUsername(string username);
    Task<User> AddUser(User user);
    Task<User> GetUserByUserId(Guid userId);
    Task<User> GetUserByGoogle(string email);
    Task<User> GetUserByUsernameNotGoogle(string username);
    Task<User> GetUserByUsernameWithEmailCheckRegister(string username, string email);
}