using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.UserDetailDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.AdminInterfaces;

public interface IAdminRepository
{
    Task<ResponseDto> SuppenseUser(Guid id);
    Task<ICollection<User>> GetUsers(UserQueryFilter queryFilter);
}