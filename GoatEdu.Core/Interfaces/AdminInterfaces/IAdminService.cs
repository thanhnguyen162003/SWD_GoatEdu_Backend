using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.AdminDto;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.AdminInterfaces;

public interface IAdminService
{
    Task<ResponseDto> CreateUser(CreateUserRequestDto user);
    Task<ResponseDto> SuppenseUser(Guid id);
    Task<PaginatedResponse<UserMinimalDto>> GetUsers(UserQueryFilter queryFilter);
}