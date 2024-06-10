using GoatEdu.Core.Interfaces.GenericInterfaces;
using GoatEdu.Core.QueriesFilter;
using Infrastructure;

namespace GoatEdu.Core.Interfaces.NoteInterfaces;

public interface INoteRepository : IRepository<Note>
{
    Task<IEnumerable<Note>> GetNoteByFilters(Guid userId, NoteQueryFilter queryFilter); 
    Task<Note?> GetNoteByUserId(Guid? guid, Guid userId);
    Task<IEnumerable<Note>> GetNoteByIds(List<Guid> ids);
    Task SoftDelete(List<Guid> guids, Guid? userId);
    

}