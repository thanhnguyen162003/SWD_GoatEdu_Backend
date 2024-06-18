using System.Net;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TranstractionDto;
using GoatEdu.Core.Interfaces;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.PaymentIntefaces;
using Infrastructure;
using Stripe.FinancialConnections;
using Transaction = Infrastructure.Transaction;

namespace GoatEdu.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;

    public PaymentService(IUnitOfWork unitOfWork, IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _claimsService = claimsService;
    }

    public async Task<ResponseDto> PaymentSuccess(TranstractionDto transaction)
    {
        var subscription = await _unitOfWork.SubcriptionRepository.GetSubscriptionById(transaction.SubcriptionId);
        if (subscription is null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "cant find any subscription in out db");
        }
        TimeSpan duration = subscription.Duration ?? new TimeSpan(30, 0, 0, 0);
        //need get userId claim here
        var userId = _claimsService.GetCurrentUserId;
        var user = await _unitOfWork.UserDetailRepository.GetByIdAsync(userId);
        
        Transaction transactionReal = new Transaction()
        {
            Note = transaction.note,
            CreatedAt = transaction.createdAt,
            TransactionName = transaction.transtractionName,
            IsDeleted = false,
            SubscriptionId = transaction.SubcriptionId,
            EndDate = DateTime.Now.Add(duration),
            WalletId = user.WalletId
        };
        User userData = new User()
        {
            Subscription = subscription.SubscriptionName,
            SubscriptionEnd = transactionReal.EndDate
        };
        var calculateUser = await _unitOfWork.UserDetailRepository.UpdateSubscription(userData);
        if (calculateUser is null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "Some things has error in adding subscription to user");
        }
        return await _unitOfWork.TranstractionRepository.AddTranstraction(transactionReal);
    }
}