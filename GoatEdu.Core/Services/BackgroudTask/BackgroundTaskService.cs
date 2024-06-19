using GoatEdu.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoatEdu.Core.Services.BackgroudTask;

public class BackgroundTaskService
{
    private readonly ILogger<BackgroundTaskService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public BackgroundTaskService(ILogger<BackgroundTaskService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateCalculationAsync()
    {
        await _unitOfWork.RateRepository.GetNumberRating();
        _logger.LogInformation(
            "Background Service updated Rating for Flashcard!!!");
    }

    public async Task VACFlashcard()
    {
        await _unitOfWork.FlashcardRepository.DisableUnder4TagsFlashcard();
        _logger.LogInformation("Background Service do VAC Flashcard!!!");
    }
    
    public async Task VACDiscussion()
    {
        // await _unitOfWork.FlashcardRepository.DisableUnder4TagsFlashcard();
        _logger.LogInformation("Background Service do VAC Discussion!!!");
    }
}