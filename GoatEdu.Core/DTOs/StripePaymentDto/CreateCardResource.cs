namespace GoatEdu.Core.DTOs.StripePaymentDto;

public record CreateCardResource(
    string Name, 
    string Number, 
    string ExpiryYear, 
    string ExpiryMonth, 
    string Cvc);