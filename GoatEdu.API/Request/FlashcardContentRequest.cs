namespace GoatEdu.API.Request;

public class FlashcardContentRequest
{
    public Guid? Id { get; set; }
    public string? flashcardContentQuestion { get; set; }
    public string? flashcardContentAnswer { get; set; }
    public string? image { get; set; }
}