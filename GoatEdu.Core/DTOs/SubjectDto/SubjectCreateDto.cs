using GoatEdu.Core.DTOs.ChapterDto;

namespace GoatEdu.Core.DTOs.SubjectDto;

public class SubjectCreateDto
{
    public Guid Id { get; set; } 
    public string? SubjectName { get; set; } 
    public string? Image { get; set; } 
    public string? SubjectCode { get; set; } 
    public string? Information { get; set; } 
    public string? Class { get; set; } 
   
}