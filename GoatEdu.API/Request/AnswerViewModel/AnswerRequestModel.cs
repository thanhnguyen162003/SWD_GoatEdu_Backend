using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GoatEdu.API.Request;

public class AnswerRequestModel
{
    [Required(ErrorMessage = "Answer body is required.")]
    public string? AnswerBody { get; set; }
    [Required(ErrorMessage = "Answer body html is required.")]
    public string? AnswerBodyHtml { get; set; }
    [Required(ErrorMessage = "Question id is required.")]
    public Guid? QuestionId { get; set; }
}