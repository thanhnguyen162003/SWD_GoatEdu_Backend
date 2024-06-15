using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.VoteInterface;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/vote")]
[ApiController]
public class VoteController : ControllerBase
{
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost("{id}")]
    public async Task<ResponseDto> DiscussionVoting([FromRoute, Required] Guid id)
    {
        return await _voteService.DiscussionVoting(id);
    }
    
    [HttpPost("answer/{id}")]
    public async Task<ResponseDto> AnswerVoting([FromRoute, Required] Guid id)
    {
        return await _voteService.AnswerVoting(id);
    }
}