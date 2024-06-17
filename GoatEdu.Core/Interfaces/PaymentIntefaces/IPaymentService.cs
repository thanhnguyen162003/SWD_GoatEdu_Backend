using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TranstractionDto;

namespace GoatEdu.Core.Interfaces.PaymentIntefaces;

public interface IPaymentService
{
    Task<ResponseDto> PaymentSuccess(TranstractionDto transaction);
}