using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace GoatEdu.API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class OpenAIController : ControllerBase
{
    [HttpGet]
    [Route("/OpenAI")]
    public async Task<IActionResult> UseChatGPT(string query)
    {
        string outputResult = "";
        var openai = new OpenAIAPI("OpenAI:APIKey");
        CompletionRequest completionRequest = new CompletionRequest();
        completionRequest.Prompt = query;
        //no money just use davinciText model pls
        completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return Ok(outputResult);

    }
}
