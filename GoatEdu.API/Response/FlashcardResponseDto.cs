namespace GoatEdu.API.Response;

public class FlashcardResponseDto
{
    public Guid id { get; set; }
    public string flashcardName { get; set; }
    public string flashcardDescription { get; set; }
    public int star { get; set; }
    public string fullName { get; set; }
    public int numberOfFlashcardContent { get; set; }

}