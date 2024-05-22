using GoatEdu.Core.DTOs.ChapterDto;
using Infrastructure;

namespace GoatEdu.Core.DTOs.SubjectDto;

public class SubjectResponseDto
{
    public Guid Id { get; set; } 
    public string? SubjectName { get; set; } 
    public string? Image { get; set; } 
    public string? SubjectCode { get; set; } 
    public string? Information { get; set; } 
    public string? Class { get; set; } 

    public DateTime? CreatedAt { get; set; } 

    public ICollection<ChapterSubjectDto>? Chapters { get; set; } 

}