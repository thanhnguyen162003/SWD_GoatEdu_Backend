
namespace GoatEdu.API.Response;

public class ChapterResponseModel
{
    public Guid Id { get; set; } 
    public string? ChapterName { get; set; } 
    public int? ChapterLevel { get; set; } 
    public Guid? SubjectId { get; set; } 
    public DateTime? CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; } 

    // public ICollection<LessonDto>? Lessons { get; set; }

    
}