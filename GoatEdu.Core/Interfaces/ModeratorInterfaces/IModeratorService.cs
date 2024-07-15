using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.ModeratorInterfaces;

public interface IModeratorService
{
    Task<ResponseDto> ApprovedDiscussion(Guid discussionId);
    Task<PagedList<SubjectDto>> GetSubjectByClass(SubjectQueryFilter queryFilter, string classes);
    Task<SubjectDto> GetSubjectBySubjectId(Guid id);
    

}