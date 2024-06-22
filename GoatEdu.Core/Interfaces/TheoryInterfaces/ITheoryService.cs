using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.TheoryInterfaces;

public interface ITheoryService
{
    Task<ResponseDto> CreateTheory(TheoryDto dto);
    Task<ResponseDto> UpdateTheory(Guid theoryId, TheoryDto dto);
    Task<ResponseDto> DeleteTheory(IEnumerable<Guid> guids);
    Task<TheoryDto?> GetTheoryById(Guid theoryId);
    Task<PagedList<TheoryDto>> GetTheoriesByFilter(Guid? lessonId, TheoryQueryFilter queryFilter);
}