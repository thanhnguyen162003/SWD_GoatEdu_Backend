using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GoatEdu.Core.Services.BackgroudTask;

public class FlashcardHostedService : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromDays(1);
    private readonly ILogger<PeriodicHostedService> _logger;
    private readonly IServiceScopeFactory _factory;
    private int _executionCount = 0;
    public bool IsEnabled { get; set; }

    public FlashcardHostedService(ILogger<PeriodicHostedService> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (
            !stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (!IsEnabled)
                {
                    await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                    BackgroundTaskService sampleService = asyncScope.ServiceProvider.GetRequiredService<BackgroundTaskService>();
                    await sampleService.VACFlashcard();
                    _executionCount++;
                    _logger.LogInformation(
                        $"Executed PeriodicHostedService - Count: {_executionCount}");
                }
                else
                {
                    _logger.LogInformation(
                        "Skipped PeriodicHostedService");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(
                    $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
            }
        }
    }
}