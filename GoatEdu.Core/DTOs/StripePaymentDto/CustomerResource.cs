namespace GoatEdu.Core.DTOs.StripePaymentDto;

public record CustomerResource(
    string CustomerId, 
    string Email, 
    string Name);