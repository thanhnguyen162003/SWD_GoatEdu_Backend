using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
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

    public async Task<ICollection<RoleDto>> GetAllRole()
    {
        return await _entities
            .Where(x => x.IsDeleted == false)
            .Select(x => new RoleDto()
            {
                Id = x.Id,
                RoleName = x.RoleName
            })
            .ToListAsync();
    }

    public async Task<RoleDto> GetRoleByRoleId(Guid id)
    {
        return await _entities.Where(
            x => x.Id == id && x.IsDeleted == false).Select(x => new RoleDto()
        {
            Id = x.Id,
            RoleName = x.RoleName
        }).FirstOrDefaultAsync();
    }

    public async Task<RoleDto> GetRoleByRoleName(string roleName)
    {
        return await _entities.Where(
            x => x.RoleName == roleName && x.IsDeleted == false).Select(x => new RoleDto()
        {
            Id = x.Id,
            RoleName = x.RoleName
        }).FirstOrDefaultAsync();
    }
    
}