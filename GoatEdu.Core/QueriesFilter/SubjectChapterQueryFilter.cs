namespace GoatEdu.Core.QueriesFilter;

public class SubjectChapterQueryFilter
{
        public Guid subject_id { get; set; }
        public string sort { get; set; } = "date";
        public string sort_direction { get; set; } = "desc";
        public string? search { get; set; }
        public int page_size { get; set; }
        public int page_number { get; set; }
    
}