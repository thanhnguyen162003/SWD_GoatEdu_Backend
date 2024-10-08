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
        var username = transaction.username;
        var user =  _unitOfWork.UserRepository.GetUserByUsername(username);
        var subscription =  _unitOfWork.SubcriptionRepository.GetSubscriptionById(transaction.SubcriptionId);
        await Task.WhenAll(user, subscription);
        if (subscription is null)
        {
            return new ResponseDto(HttpStatusCode.BadRequest, "cant find any subscription in out db");
        }
        TimeSpan duration = subscription.Result.Duration ?? new TimeSpan(30, 0, 0, 0);
        Transaction transactionReal = new Transaction()
        {
            Note = transaction.note,
            CreatedAt = transaction.createdAt,
            TransactionName = transaction.transtractionName,
            IsDeleted = false,
            SubscriptionId = transaction.SubcriptionId,
            EndDate = DateTime.Now.Add(duration),
            StartDate = DateTime.Now,
            WalletId = user.Result.WalletId
        };
        User userData = new User()
        {
            Id = user.Result.Id,
            Subscription = subscription.Result.SubscriptionName,
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