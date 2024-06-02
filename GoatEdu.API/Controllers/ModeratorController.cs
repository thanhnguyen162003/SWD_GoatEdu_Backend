using System.ComponentModel.DataAnnotations;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.ModeratorInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/moder")]
[ApiController]
[Authorize(Roles = UserEnum.MODERATOR)]
public class ModeratorController : ControllerBase
{
    private readonly IModeratorService _moderatorService;
    
    public ModeratorController(IModeratorService moderatorService)
    {
        _moderatorService = moderatorService;
    }
    
    [HttpPost("discussion")]
    public async Task<IActionResult> ApproveDiscussion([Required]List<Guid> ids)
    {
        try
        {
            var result = await _moderatorService.ApprovedDiscussion(ids);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}