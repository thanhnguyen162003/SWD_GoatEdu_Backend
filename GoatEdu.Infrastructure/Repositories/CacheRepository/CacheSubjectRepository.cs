using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.RoleDto;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
using GoatEdu.Core.Models;
using GoatEdu.Core.QueriesFilter;
using Infrastructure.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Repositories.CacheRepository;

public class CacheSubjectRepository : ISubjectRepository
{
    private readonly SubjectRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly GoatEduContext _context;

    public CacheSubjectRepository(SubjectRepository decorated, IDistributedCache distributedCache, GoatEduContext context)
    {
        _decorated = decorated as SubjectRepository ?? throw new ArgumentException("Decorated repository must be of type SubjectRepository", nameof(decorated));
        _distributedCache = distributedCache;
        _context = context;
    }
    public async Task<ICollection<Subject>> GetAllSubjects(SubjectQueryFilter queryFilter)
    {
        string key = "all-subject";
        string? cachedSubjects = await _distributedCache.GetStringAsync(key);
        
        if (!string.IsNullOrEmpty(cachedSubjects))
        {
            return JsonConvert.DeserializeObject<ICollection<Subject>>(cachedSubjects)!; // Null-forgiving operator
        }

        var roles = await _decorated.GetAllSubjects(queryFilter);
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(roles));
        return roles;
    }

    public async Task<SubjectResponseDto> GetSubjectBySubjectId(Guid id)
    {
        string key = $"subject-{id}";
        string? cacheSubject = await _distributedCache.GetStringAsync(key);
        SubjectResponseDto subject;
        if (string.IsNullOrEmpty(cacheSubject))
        {
            subject = await _decorated.GetSubjectBySubjectId(id);
            if (subject is null)
            {
                return subject;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(subject));
            return subject;
        }

        subject = JsonConvert.DeserializeObject<SubjectResponseDto>(cacheSubject,
            // tell that it need to find constructor that dont have public or private default
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            }
        );
        return subject;
    }

    public async Task<ResponseDto> DeleteSubject(Guid id)
    {
        // Perform delete operation
        var response = await _decorated.DeleteSubject(id);

        // Invalidate related cache entries if needed
        string key = $"subject-{id}";
        await _distributedCache.RemoveAsync(key);

        return response;
    }

    public async Task<ResponseDto> UpdateSubject(SubjectCreateDto dto)
    {
        // Perform update operation
        var response = await _decorated.UpdateSubject(dto);

        // Invalidate related cache entries if needed
        string key = $"subject-{dto.Id}";
        await _distributedCache.RemoveAsync(key);

        return response;
    }

    public async Task<ResponseDto> CreateSubject(Subject dto)
    {
        // Perform create operation
        var response = await _decorated.CreateSubject(dto);

        // No need to cache create operations

        return response;
    }

    public async Task<SubjectResponseDto> GetSubjectBySubjectName(string subjectName)
    {
        string key = $"subject-{subjectName}";
        string? cacheSubject = await _distributedCache.GetStringAsync(key);
        SubjectResponseDto subject;
        if (string.IsNullOrEmpty(cacheSubject))
        {
            subject = await _decorated.GetSubjectBySubjectName(subjectName);
            if (subject is null)
            {
                return subject;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(subject));
            return subject;
        }

        subject = JsonConvert.DeserializeObject<SubjectResponseDto>(cacheSubject,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            }
        );
        return subject;
    }
}