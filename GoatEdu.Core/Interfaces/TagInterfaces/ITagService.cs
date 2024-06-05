using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.TagInterfaces;

public interface ITagService
{
    Task<PagedList<TagDto>> GetTagByFilter(TagQueryFilter queryFilter);
    Task<ResponseDto> getTagByName(List<string> name);
    Task<ResponseDto> GetTagById(Guid guid);
    Task<ResponseDto> InsertTags (List<TagDto> tagRequestDtos);
    Task<ResponseDto> DeleteTags(List<Guid> guids);
    Task<ResponseDto> UpdateTag(Guid guid, TagDto tagRequestDto);
}