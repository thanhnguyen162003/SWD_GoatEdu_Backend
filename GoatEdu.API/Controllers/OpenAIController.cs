using GoatEdu.API.Request.OpenAIRequestModel;
using GoatEdu.API.Response;
using GoatEdu.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace GoatEdu.API.Controllers;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class OpenAIController : ControllerBase
{
    
    private readonly IADProductService _adProduct;

    public OpenAIController(IADProductService adProduct)
    {
        _adProduct = adProduct;
    }
    [HttpPost("OpenAI")]
    public async Task<ActionResult<ADProductResponseModel>> GenerateAD(CustomerRequestModel aDGenerateRequestModel)
    {
        try
        {

            var response = await _adProduct.GenerateAdContent(aDGenerateRequestModel);

            return response;
        }
        catch (System.Exception ex)
        {

            return null;
        }

    }
}
