using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NoteInterfaces;

public interface INoteService
{
    Task<ResponseDto> GetNoteById(Guid id);
    Task<PagedList<NoteResponseDto>> GetNoteByFilter(NoteQueryFilter queryFilter);
    Task<ResponseDto> InsertNote(NoteRequestDto noteRequestDto);
    Task<ResponseDto> DeleteNotes(List<Guid> guids);
    Task<ResponseDto> UpdateNote(Guid guid, NoteRequestDto noteRequestDto);
}