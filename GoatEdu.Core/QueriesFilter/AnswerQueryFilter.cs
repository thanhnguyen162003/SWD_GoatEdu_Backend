namespace GoatEdu.Core.QueriesFilter;

public class AnswerQueryFilter
{
    public string sort_direction { get; set; } = "desc";
    public int page_size { get; set; }
    public int page_number { get; set; }
}