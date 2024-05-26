namespace GoatEdu.Core.QueriesFilter;

public class TagQueryFilter
{
    public string Sort { get; set; } = "name";
    public string SortDirection { get; set; } = "desc";
    public string? Search { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}