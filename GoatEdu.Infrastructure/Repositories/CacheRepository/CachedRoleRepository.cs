using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Repositories.CacheRepository;

public class CachedRoleRepository : IRoleRepository
{
    private readonly RoleRepository _decorated;
    private readonly IDistributedCache _distributedCache;

    public CachedRoleRepository(RoleRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;
    }
    public Task<ICollection<RoleResponseDto>> GetAllRole()
    {
        throw new NotImplementedException();
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

    public Task<RoleResponseDto> GetRoleByRoleName(string roleName)
    {
        throw new NotImplementedException();
    }
}