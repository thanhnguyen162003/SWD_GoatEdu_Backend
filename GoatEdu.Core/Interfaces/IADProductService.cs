using GoatEdu.API.Request.OpenAIRequestModel;
using GoatEdu.API.Response;

namespace GoatEdu.Core.Interfaces;

public interface IADProductService
{
    Task<ADProductResponseModel> GenerateAdContent(CustomerRequestModel aDGenerateRequestModel);
}
