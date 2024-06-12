namespace GoatEdu.Core.DTOs.ChapterDto;

public class ChapterDto
{
    public Guid? Id { get; set; } 
    public string? ChapterName { get; set; }
    public int? ChapterLevel { get; set; }
    public Guid? SubjectId { get; set; }
    public DateTime? CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; } 

}