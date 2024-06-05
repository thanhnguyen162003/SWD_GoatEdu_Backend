using GoatEdu.Core.DTOs.TagDto;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.API.Request;

public class DiscussionRequestDto
{
    public DiscussionRequestDto(List<TagRequestDto> tags)
    {
        Tags = tags;
    }

    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public IFormFile? DiscussionImage { get; set; }
    public List<TagRequestDto> Tags { get; set; }
    public Guid? SubjectId { get; set; }
    public bool? IsSolved { get; set; }
}