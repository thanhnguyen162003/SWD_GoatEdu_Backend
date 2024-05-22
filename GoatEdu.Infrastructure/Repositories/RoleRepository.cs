using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    private readonly GoatEduContext _context;

    public RoleRepository(GoatEduContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<RoleResponseDto>> GetAllRole()
    {
        return await _entities
            .Where(x => x.IsDeleted == false)
            .Select(x => new RoleResponseDto()
            {
                Id = x.Id,
                RoleName = x.RoleName
            })
            .ToListAsync();
    }

    public async Task<RoleResponseDto> GetRoleByRoleId(Guid id)
    {
        return await _entities.Where(
            x => x.Id == id && x.IsDeleted == false).Select(x => new RoleResponseDto()
        {
            Id = x.Id,
            RoleName = x.RoleName
        }).FirstOrDefaultAsync();
    }

    public async Task<RoleResponseDto> GetRoleByRoleName(string roleName)
    {
        return await _entities.Where(
            x => x.RoleName == roleName && x.IsDeleted == false).Select(x => new RoleResponseDto()
        {
            Id = x.Id,
            RoleName = x.RoleName
        }).FirstOrDefaultAsync();
    }
}