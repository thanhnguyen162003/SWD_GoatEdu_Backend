namespace GoatEdu.Core.DTOs.ChapterDto;

public class ChapterSubjectDto
{
    public Guid? Id { get; set; }
    public string? ChapterName { get; set; }
    public int? ChapterLevel { get; set; }
    public Guid? SubjectId { get; set; } 
    public DateTime? CreatedAt { get; set; } 
}