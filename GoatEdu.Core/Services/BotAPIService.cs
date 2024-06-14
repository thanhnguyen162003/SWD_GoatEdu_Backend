using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenAI_API;

namespace GoatEdu.Core.Services;

public class BotAPIService : IBotAPIService
{
    private readonly IConfiguration _configuration;

    public BotAPIService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<string>> GenerateContent(ADGenerateRequestModelDTO generateRequestModel)
    {
        var apiKey = _configuration.GetSection("OpenAI:APIKey").Value;
        var apiModel = _configuration.GetSection("OpenAI:Model").Value;
        List<string> rq = new List<string>();
        string rs = "";
        OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));
        var completionRequest = new OpenAI_API.Completions.CompletionRequest()
        {
            Prompt = generateRequestModel.prompt,
            Model = apiModel,
            Temperature = 0.5,
            MaxTokens = 100,
            TopP = 1.0,
            FrequencyPenalty = 0.0,
            PresencePenalty = 0.0,

        };
        var result = await api.Completions.CreateCompletionsAsync(completionRequest);
        foreach (var choice in result.Completions)
        {
            rs = choice.Text;
            rq.Add(choice.Text);
        }

        return rq;
    }
}