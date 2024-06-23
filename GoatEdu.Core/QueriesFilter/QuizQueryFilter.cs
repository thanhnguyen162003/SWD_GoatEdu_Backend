using System.ComponentModel.DataAnnotations;

namespace GoatEdu.Core.QueriesFilter;

public class QuizQueryFilter
{
    public string sort { get; set; } = "quizLevel";
    public string sort_direction { get; set; } = "asc";
    public Guid? id { get; set; }
    public string? type { get; set; }
    public int page_size { get; set; }
    public int page_number { get; set; }
}