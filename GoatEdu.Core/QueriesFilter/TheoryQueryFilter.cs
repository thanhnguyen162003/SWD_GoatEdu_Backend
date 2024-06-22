namespace GoatEdu.Core.QueriesFilter;

public class TheoryQueryFilter
{
    public string sort { get; set; } = "date";
    public string sort_direction { get; set; } = "asc";
    public int page_size { get; set; }
    public int page_number { get; set; }
}