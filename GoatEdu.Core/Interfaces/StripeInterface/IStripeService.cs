using GoatEdu.Core.DTOs.StripePaymentDto;

namespace GoatEdu.Core.Interfaces.StripeInterface;

public interface IStripeService
{
    Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
    Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);
}