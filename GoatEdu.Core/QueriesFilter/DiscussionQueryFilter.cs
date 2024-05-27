namespace GoatEdu.Core.QueriesFilter;

public class DiscussionQueryFilter
{
    public string Sort { get; set; } = "date";
    public string SortDirection { get; set; } = "desc";
    public List<string?> TagNames { get; set; } = null!;
    public string? Search { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}