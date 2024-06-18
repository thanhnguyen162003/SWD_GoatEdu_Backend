using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.DiscussionInterfaces;

public interface IDiscussionService
{
    Task<PagedList<DiscussionDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter);
    Task<DiscussionDto?> GetDiscussionById(Guid guid);
    Task<PagedList<DiscussionDto>> GetDiscussionByUserId(DiscussionQueryFilter queryFilter);
    Task<ResponseDto> CreateDiscussion (DiscussionDto dto);
    Task<ResponseDto> DeleteDiscussions(List<Guid> guids);
    Task<ResponseDto> UpdateDiscussion(Guid guid, DiscussionDto dto);
}