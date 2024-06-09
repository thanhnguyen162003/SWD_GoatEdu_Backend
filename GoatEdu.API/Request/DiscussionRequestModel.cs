using GoatEdu.Core.DTOs.TagDto;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.API.Request;

public class DiscussionRequestModel
{
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public IFormFile? DiscussionImage { get; set; }
    public List<TagRequestModel> Tags { get; set; }
    public Guid? SubjectId { get; set; }
    public bool? IsSolved { get; set; }
}