using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.ModeratorInterfaces;

public interface IModeratorRepository
{
    Task<Guid?> ApprovedDiscussions(Guid discussionId);
    Task<SubjectDto> GetSubjectBySubjectId(Guid id);
    Task<IEnumerable<Subject>> GetSubjectByClass(string classes, SubjectQueryFilter queryFilter);

}