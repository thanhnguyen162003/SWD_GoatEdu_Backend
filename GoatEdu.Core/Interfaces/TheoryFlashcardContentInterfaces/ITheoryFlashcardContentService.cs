using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;

namespace GoatEdu.Core.Interfaces.TheoryFlashcardContentInterfaces;

public interface ITheoryFlashcardContentService
{
    Task<ResponseDto> CreateTheTheoryFlashcardContent(Guid theoryId, List<TheoryFlashcardContentsDto> dtos);
    Task<ResponseDto> UpdateTheTheoryFlashcardContent(Guid theoryId,List<TheoryFlashcardContentsDto> dtos);
    Task<ResponseDto> DeleteTheTheoryFlashcardContent(List<Guid> guids);
    Task<IEnumerable<TheoryFlashcardContentsDto>> GetTheoryFlashcardContentsByTheory(Guid theoryId);
}