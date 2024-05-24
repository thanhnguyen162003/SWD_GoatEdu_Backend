using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Repositories.CacheRepository;

public class CachedRoleRepository : IRoleRepository
{
    private readonly RoleRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly GoatEduContext _context;

    public CachedRoleRepository(IRoleRepository decorated, IDistributedCache distributedCache, GoatEduContext context)
    {
        _decorated = decorated as RoleRepository ?? throw new ArgumentException("Decorated repository must be of type RoleRepository", nameof(decorated));
        _distributedCache = distributedCache;
        _context = context;
    }

    public async Task<ICollection<RoleResponseDto>> GetAllRole()
    {
        string key = "all-roles";
        string? cachedRoles = await _distributedCache.GetStringAsync(key);
        
        if (!string.IsNullOrEmpty(cachedRoles))
        {
            return JsonConvert.DeserializeObject<ICollection<RoleResponseDto>>(cachedRoles)!; // Null-forgiving operator
        }

        var roles = await _decorated.GetAllRole();
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(roles));
        return roles;
    }

    public async Task<RoleResponseDto> GetRoleByRoleId(Guid id)
    {
        string key = $"role-{id}";
        string? cachedRole = await _distributedCache.GetStringAsync(key);
        RoleResponseDto role;
        if (string.IsNullOrEmpty(cachedRole))
        {
             role = await _decorated.GetRoleByRoleId(id);
            if (role is null)
            {
                return role;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(role));
            return role;
        }

        role = JsonConvert.DeserializeObject<RoleResponseDto>(cachedRole,
            // tell that it need to find constructor that dont have public or private default
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            }
            );
        return role;
    }

    public async Task<RoleResponseDto> GetRoleByRoleName(string roleName)
    {
        string key = $"role-{roleName}";
        string? cachedRole = await _distributedCache.GetStringAsync(key);
        RoleResponseDto role;
        if (string.IsNullOrEmpty(cachedRole))
        {
            role = await _decorated.GetRoleByRoleName(roleName);
            if (role is null)
            {
                return role;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(role));
            return role;
        }

        role = JsonConvert.DeserializeObject<RoleResponseDto>(cachedRole,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            }
        );
        return role;
    }
}