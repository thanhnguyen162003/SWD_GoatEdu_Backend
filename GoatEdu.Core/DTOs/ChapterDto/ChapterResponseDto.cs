using GoatEdu.Core.Models;

namespace GoatEdu.Core.DTOs.ChapterDto;

public class ChapterResponseDto
{
    public Guid Id { get; set; } 
    public string? ChapterName { get; set; } 
    public int? ChapterLevel { get; set; } 
    public Guid? SubjectId { get; set; } 
    public DateTime? CreatedAt { get; set; } 
    
    public ICollection<Lesson>? Lessons { get; set; }

    
}