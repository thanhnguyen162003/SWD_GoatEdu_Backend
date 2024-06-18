namespace GoatEdu.Core.QueriesFilter;

public class LessonQueryFilter
{
    public string sort { get; set; } = "order";
    public string sort_direction { get; set; } = "asc";
    public int page_size { get; set; }
    public int page_number { get; set; }
}