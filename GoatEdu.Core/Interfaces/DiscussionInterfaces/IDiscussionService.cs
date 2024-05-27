using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.DiscussionInterfaces;

public interface IDiscussionService
{
    Task<PagedList<DiscussionResponseDto>> GetDiscussionByFilter(DiscussionQueryFilter queryFilter);
    Task<ResponseDto> GetDiscussionById(Guid guid);
    Task<PagedList<DiscussionResponseDto>> GetDiscussionByUserId(DiscussionQueryFilter queryFilter);
    Task<ResponseDto> InsertDiscussion (DiscussionRequestDto discussionRequestDto);
    Task<ResponseDto> DeleteDiscussions(List<Guid> guids);
    Task<ResponseDto> UpdateDiscussion(Guid guid, DiscussionRequestDto discussionRequestDto);
}