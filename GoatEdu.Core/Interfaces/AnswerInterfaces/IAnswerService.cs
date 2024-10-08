using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Interfaces.AnswerInterfaces;

public interface IAnswerService
{
    Task<ResponseDto> InsertAnswer(AnswerDto dto);
    Task<ResponseDto> DeleteAnswer(Guid answerId);
    Task<PagedList<AnswerDto>> GetByDiscussionId(Guid guid, AnswerQueryFilter queryFilter);
    Task<ResponseDto> UpdateAnswer(Guid answerId, AnswerDto dto);
}