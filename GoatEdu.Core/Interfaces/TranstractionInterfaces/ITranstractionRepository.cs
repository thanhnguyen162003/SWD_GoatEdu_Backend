using GoatEdu.Core.DTOs;
using Stripe.FinancialConnections;
using Transaction = Infrastructure.Transaction;

namespace GoatEdu.Core.Interfaces.TranstractionInterfaces;

public interface ITranstractionRepository
{
    Task<ResponseDto> AddTranstraction(Transaction transaction);
}