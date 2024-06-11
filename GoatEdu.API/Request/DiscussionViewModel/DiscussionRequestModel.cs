using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs.TagDto;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.API.Request;

public class DiscussionRequestModel
{
    [Required(ErrorMessage = "Discussion name is required!")]
    public string? DiscussionName { get; set; }
    [Required(ErrorMessage = "Discussion body is required!")]
    public string? DiscussionBody { get; set; }
    public IFormFile? DiscussionImage { get; set; }
    [Required(ErrorMessage = "Tags is required!")]
    public List<TagUpdateModel>? Tags { get; set; }
    [Required(ErrorMessage = "Subject id is required!")]
    public Guid? SubjectId { get; set; }
}