using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

[Route("api/payment")]
[ApiController]
public class PaymentsController : Controller
{
    private readonly ILogger<PaymentsController> _logger;
    const string endpointSecret = "whsec_HgktjnRdJz4sg1479qWJoMtj9YWPjEDT";

    public PaymentsController(ILogger<PaymentsController> logger)
    {
        _logger = logger;
    }

    // [Authorize]
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
            SuccessUrl = "https://goatedu.vercel.app/browse",
            CancelUrl = "https://goatedu.vercel.app",
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
                Request.Headers["Stripe-Signature"], endpointSecret);

            // Handle the event
            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var session = stripeEvent.Data.Object as Session;
                _logger.LogInformation("Payment successful. Session ID: " + session.Id);

                // Process the session
                // Here you can save the session details to your database or perform other necessary actions
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
