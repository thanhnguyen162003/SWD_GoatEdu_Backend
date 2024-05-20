namespace GoatEdu.Core.QueriesFilter;

public class NoteQueryFilter
{
    public Guid UserId { get; set; }
    public string Sort { get; set; } = "date";
    public string SortDirection { get; set; } = "desc";
    public string? Search { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}