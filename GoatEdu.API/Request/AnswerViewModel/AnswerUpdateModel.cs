namespace GoatEdu.API.Request;

public class AnswerUpdateModel
{
    public string? AnswerBody { get; set; }
    public string? AnswerBodyHtml { get; set; }
    public IFormFile? AnswerImage { get; set; }
}