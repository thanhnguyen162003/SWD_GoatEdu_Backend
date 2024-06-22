using GoatEdu.Core.DTOs.TheoryFlashcardDto;
using Infrastructure;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs.TheoryDto;

public class TheoryDto
{
    public Guid? Id { get; set; }
    public string? TheoryName { get; set; }
    public string? TheoryContent { get; set; }
    public string? File { get; set; }
    public IFormFile? FormFile { get; set; }
    public string? Image { get; set; }
    public IFormFile? ImageFile { get; set; }
    public Guid? LessonId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TheoryFlashcardContentsDto>? TheoryFlashCardContents { get; set; }
}