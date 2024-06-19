using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.IO;
using System.Threading.Tasks;
using GoatEdu.Core.DTOs.TranstractionDto;
using GoatEdu.Core.Interfaces.ClaimInterfaces;
using GoatEdu.Core.Interfaces.PaymentIntefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

[Route("api/payment")]
[ApiController]
public class PaymentsController : Controller
{
    private readonly ILogger<PaymentsController> _logger;
    private readonly IClaimsService _claimsService;
    const string endpointSecret = "whsec_zDoM7pBkZdEKJPE7Hrz4s7jfKRCFoI5l";
    private readonly IPaymentService _paymentService;
    private Guid ProSubcriptionMonth = new Guid("d94d0651-6a18-45d0-a0f9-bc00ea69b584");

    public PaymentsController(ILogger<PaymentsController> logger, IPaymentService paymentService, IClaimsService claimsService)
    {
        _logger = logger;
        _paymentService = paymentService;
        _claimsService = claimsService;
    }

    [Authorize]
    [HttpPost("create-checkout-session")]
    public ActionResult CreateCheckoutSession()
    {
        var customdata = _claimsService.GetCurrentUsername;
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = 399,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Pro GoatEdu",
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
           
            Metadata = new Dictionary<string, string>
            {
                { "CustomEmail", customdata }
            },
            SuccessUrl = "https://goatedu.vercel.app/payment/success",
            CancelUrl = "https://goatedu.vercel.app/payment/cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Redirect(session.Url);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
        string customName;
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                endpointSecret,
                throwOnApiVersionMismatch: false
            );

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;

                if (session != null && session.Metadata.TryGetValue("CustomEmail", out var customEmail))
                {
                    _logger.LogInformation($"CustomEmail: {customEmail}");

                    TranstractionDto transtractionDto = new TranstractionDto()
                    {
                        transtractionName = "Pro Service 1 Month",
                        createdAt = DateTime.Now,
                        note = "is in Development",
                        SubcriptionId = ProSubcriptionMonth,
                        username = customEmail // Use the email from metadata
                    };

                    await _paymentService.PaymentSuccess(transtractionDto);
                }
            }
            return Ok();
        }
        catch (StripeException e)
        {
            _logger.LogError(e, "Stripe webhook failed");
            return BadRequest();
        }
    }
}
