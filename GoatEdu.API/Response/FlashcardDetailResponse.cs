namespace GoatEdu.API.Response;

public class FlashcardDetailResponse
{
        public Guid id { get; set; }
        public string flashcardName { get; set; }
        public string flashcardDescription { get; set; }
        public int star { get; set; }
        public string fullName { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string userImage { get; set; }
        public int numberOfFlashcardContent { get; set; }
}