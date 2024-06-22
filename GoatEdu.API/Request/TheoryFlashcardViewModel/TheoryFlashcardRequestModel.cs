using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request.TheoryFlashcardViewModel;

public class TheoryFlashcardRequestModel
{
    [Required(ErrorMessage = "Question is required!")]
    public string? Question { get; set; }
    [Required(ErrorMessage = "Answer is required!")]
    public string? Answer { get; set; }
}