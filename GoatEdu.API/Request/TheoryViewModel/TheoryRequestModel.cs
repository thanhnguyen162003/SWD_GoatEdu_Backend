using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;

namespace GoatEdu.API.Request.TheoryViewModel;

public class TheoryRequestModel
{    
    [Required(ErrorMessage = "Theory name is required!")]
    public string? TheoryName { get; set; }
    [Required(ErrorMessage = "Theory content is required!")]
    public string? TheoryContent { get; set; }
    public IFormFile? FormFile { get; set; }
    public IFormFile? ImageFile { get; set; } 
    [Required(ErrorMessage = "Lesson id is required!")]
    public Guid? LessonId { get; set; }
}