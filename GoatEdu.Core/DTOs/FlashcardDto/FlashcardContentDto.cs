namespace GoatEdu.Core.DTOs.FlashcardDto;

public class FlashcardContentDto
{
    public string flashcardContentQuestion { get; set; }
    public string flashcardContentAnswer { get; set; }
    public string? image { get; set; }
    public Guid id { get; set; }
}