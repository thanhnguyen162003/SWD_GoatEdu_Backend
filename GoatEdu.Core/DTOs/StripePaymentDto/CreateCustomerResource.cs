namespace GoatEdu.Core.DTOs.StripePaymentDto;

public record CreateCustomerResource(
    string Email, 
    string Name, 
    CreateCardResource Card);