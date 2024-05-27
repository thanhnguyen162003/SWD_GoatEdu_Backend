using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoatEdu.API.Controllers;

[Route("api/admin")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    
}