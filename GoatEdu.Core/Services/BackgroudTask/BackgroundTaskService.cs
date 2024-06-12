using Microsoft.Extensions.Logging;

namespace GoatEdu.Core.Services.BackgroudTask;

public class BackgroundTaskService
{
    private readonly ILogger<BackgroundTaskService> _logger;

    public BackgroundTaskService(ILogger<BackgroundTaskService> logger)
    {
        _logger = logger;
    }

    public async Task UpdateCalculationAsync()
    {
        await Task.Delay(100);
        _logger.LogInformation(
            "Background Service updated Calculation!!!");
    }
}