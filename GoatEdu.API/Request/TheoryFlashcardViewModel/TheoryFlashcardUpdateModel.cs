using System.ComponentModel.DataAnnotations;

namespace GoatEdu.API.Request.TheoryFlashcardViewModel;

public class TheoryFlashcardUpdateModel
{
    [Required(ErrorMessage = "Theory Flashcard Id is required!")]
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public string? Status { get; set; }
}