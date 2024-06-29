using CloudinaryDotNet.Actions;
using GoatEdu.Core.DTOs.ChapterDto;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs.SubjectDto;

public class SubjectDto
{
    public Guid Id { get; set; } 
    public string? SubjectName { get; set; } 
    public string? Image { get; set; }
    public IFormFile? ImageConvert { get; set; }

    public string? SubjectCode { get; set; } 
    public string? Information { get; set; } 
    public string? Class { get; set; }
    
    public ICollection<ChapterSubjectDto>? Chapters { get; set; } 
    public int? NumberOfChapters { get; set; }
    public DateTime? CreatedAt { get; set; }
    
}