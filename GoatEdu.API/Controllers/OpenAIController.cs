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
    [HttpPost]
    [Route("/OpenAI")]
    public async Task<IActionResult> UseChatGPT(string query)
    {
        string outputResult = "";
        var openai = new OpenAIAPI("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");
        CompletionRequest completionRequest = new CompletionRequest();
        completionRequest.Prompt = query;
        //no money just use davinciText model pls
        completionRequest.Model = "gpt-3.5-turbo";
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return Ok(outputResult);

    }
}
