using GoatEdu.Core.DTOs.TagDto;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.DTOs;

public class DiscussionRequestDto
{
    public string? DiscussionName { get; set; }
    public string? DiscussionBody { get; set; }
    public IFormFile? DiscussionImage { get; set; }
    public List<TagRequestDto> Tags { get; set; }
    public Guid? SubjectId { get; set; }
    public bool? IsSolved { get; set; } = false;
}