using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.IO;
using System.Threading.Tasks;
using GoatEdu.Core.DTOs.TranstractionDto;
using GoatEdu.Core.Interfaces.PaymentIntefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

[Route("api/payment")]
[ApiController]
public class PaymentsController : Controller
{
    private readonly ILogger<PaymentsController> _logger;
    const string endpointSecret = "whsec_3cc554964386f69f0f5ac33314f1945bb7f2c77959a61cbbcae697bc39f39b66";
    private readonly IPaymentService _paymentService;
    private Guid ProSubcriptionMonth = new Guid("d94d0651-6a18-45d0-a0f9-bc00ea69b584");

    public PaymentsController(ILogger<PaymentsController> logger, IPaymentService paymentService)
    {
        _logger = logger;
        _paymentService = paymentService;
    }

    [Authorize]
    [HttpPost("create-checkout-session")]
    public ActionResult CreateCheckoutSession()
    {
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
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret, throwOnApiVersionMismatch: false);

            // Handle the event
            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var session = stripeEvent.Data.Object as Session;
                
                TranstractionDto transtractionDto = new TranstractionDto()
                {
                    transtractionName = "Pro Service 1 Month",
                    createdAt = DateTime.Now,
                    note = "is in Development",
                    SubcriptionId = ProSubcriptionMonth
                };
                await _paymentService.PaymentSuccess(transtractionDto);
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
