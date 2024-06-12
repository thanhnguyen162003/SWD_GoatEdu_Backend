using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GoatEdu.API.Request;

public class AnswerRequestModel
{
    [Required(ErrorMessage = "Answer body is required.")]
    public string? AnswerBody { get; set; }
    public string? AnswerBodyHtml { get; set; }
    public Guid? QuestionId { get; set; }
    public IFormFile? AnswerImage { get; set; }
}