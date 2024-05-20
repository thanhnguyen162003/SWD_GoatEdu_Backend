using GoatEdu.Core.Interfaces.GenericInterfaces;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NoteInterfaces;

public interface INoteRepository : IRepository<Note>
{
    Task<List<Note>> GetNoteByUserId(Guid userId);
    Task<List<Note>> GetNoteByIds(List<Guid> ids);
    Task SoftDelete(List<Guid> guids);
    

}