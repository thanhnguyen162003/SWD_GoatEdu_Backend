using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Interfaces;

public interface IBotAPIService
{
    Task<List<string>> GenerateContent(ADGenerateRequestModelDTO generateRequestModel);
}
