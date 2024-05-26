using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.TagInterfaces;

public interface ITagService
{
    Task<PagedList<TagResponseDto>> GetTagByFilter(TagQueryFilter queryFilter);
    Task<ResponseDto> GetTagById(Guid guid);
    Task<ResponseDto> InsertTags (List<TagRequestDto> tagRequestDtos);
    Task<ResponseDto> DeleteTags(List<Guid> guids);
    Task<ResponseDto> UpdateTag(Guid guid, TagRequestDto tagRequestDto);
}