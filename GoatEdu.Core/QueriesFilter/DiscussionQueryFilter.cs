using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.Enumerations;

namespace GoatEdu.Core.QueriesFilter;

public class DiscussionQueryFilter
{
    public string sort { get; set; } = "date";
    public string sort_direction { get; set; } = "desc";
    public List<string> tag_names { get; set; } = new();
    public string? search { get; set; }
    public string? status { get; set; } = StatusConstraint.APPROVED;
    public int page_size { get; set; }
    public int page_number { get; set; }
}