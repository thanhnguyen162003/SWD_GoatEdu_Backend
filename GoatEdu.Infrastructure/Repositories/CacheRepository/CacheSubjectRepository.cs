using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;
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
    public async Task<IEnumerable<Subject>> GetAllSubjects(SubjectQueryFilter queryFilter)
    {
        string key = $"all-subject-{queryFilter.page_size}-{queryFilter.page_number}-{queryFilter.search}-{queryFilter.sort}-{queryFilter.sort_direction}";
        string? cachedSubjects = await _distributedCache.GetStringAsync(key);
        
        if (!string.IsNullOrEmpty(cachedSubjects))
        {
            return JsonConvert.DeserializeObject<ICollection<Subject>>(cachedSubjects)!; // Because of redis store data in object. so we need to deserialize to get data
        }
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1000) //near 1 day expire cache
        };
        //add loop handling for serializeObject, dont know perfomance have impact or not.
        //still consider remove chapter from subject to better perfomance
        var loopHandling = new JsonSerializerSettings 
        { 
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var subjects = await _decorated.GetAllSubjects(queryFilter);
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(subjects,loopHandling),cacheOptions);
        return subjects;
    }

    public async Task<SubjectDto> GetSubjectBySubjectId(Guid id)
    {
        string key = $"subject-{id}";
        string? cacheSubject = await _distributedCache.GetStringAsync(key);
        SubjectDto subject;
        if (string.IsNullOrEmpty(cacheSubject))
        {
            subject = await _decorated.GetSubjectBySubjectId(id);
            if (subject is null)
            {
                return subject;
            }
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1000) //near 1 day expire cache
            };
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(subject),cacheOptions);
            return subject;
        }
        
        subject = JsonConvert.DeserializeObject<SubjectDto>(cacheSubject,
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

    public async Task<ResponseDto> UpdateSubject(Subject dto)
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

    public async Task<SubjectDto> GetSubjectBySubjectName(string subjectName)
    {
        string key = $"subject-{subjectName}";
        string? cacheSubject = await _distributedCache.GetStringAsync(key);
        SubjectDto subject;
        if (string.IsNullOrEmpty(cacheSubject))
        {
            subject = await _decorated.GetSubjectBySubjectName(subjectName);
            if (subject is null)
            {
                return subject;
            }
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1000) //near 1 day expire cache
            };
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(subject),cacheOptions);
            return subject;
        }

        subject = JsonConvert.DeserializeObject<SubjectDto>(cacheSubject,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            }
        );
        return subject;
    }

    public async Task<IEnumerable<Subject>> GetSubjectByClass(string classes, SubjectQueryFilter queryFilter)
    {
        string key = $"subject-classes-{classes}-{queryFilter.page_size}-{queryFilter.page_number}-{queryFilter.search}-{queryFilter.sort}-{queryFilter.sort_direction}";
        string? cachedSubjects = await _distributedCache.GetStringAsync(key);
        
        if (!string.IsNullOrEmpty(cachedSubjects))
        {
            return JsonConvert.DeserializeObject<ICollection<Subject>>(cachedSubjects)!; // Because of redis store data in object. so we need to deserialize to get data
        }
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1000) //near 1 day expire cache
        };
        var loopHandling = new JsonSerializerSettings 
        { 
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var subjects = await _decorated.GetSubjectByClass(classes, queryFilter);
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(subjects,loopHandling),cacheOptions);
        return subjects;
    }

    public async Task<bool> SubjectIdExistAsync(Guid? guid)
    {
        // No need to cache create operations
        var response = await _decorated.SubjectIdExistAsync(guid);
        return response;
    }

    public async Task<bool> SubjectNameExistAsync(string name)
    {
        // No need to cache create operations

        var response = await _decorated.SubjectNameExistAsync(name);
        return response;
    }

    public async Task<bool> SubjectCodeExistAsync(string code)
    {
        // No need to cache create operations
        var response = await _decorated.SubjectCodeExistAsync(code);
        return response;
    }
}