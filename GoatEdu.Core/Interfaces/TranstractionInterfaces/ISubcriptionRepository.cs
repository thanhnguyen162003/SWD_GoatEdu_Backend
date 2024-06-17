using Infrastructure;

namespace GoatEdu.Core.Interfaces.TranstractionInterfaces;

public interface ISubcriptionRepository
{
    Task<Subscription> GetSubscriptionById(Guid subscriptionId);
}