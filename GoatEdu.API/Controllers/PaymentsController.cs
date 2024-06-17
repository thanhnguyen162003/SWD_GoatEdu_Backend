using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace GoatEdu.API.Controllers;

public class PaymentsController : Controller
{
    public PaymentsController()
    {
        
    }

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
            SuccessUrl = "http://localhost:4242/success",
            CancelUrl = "http://localhost:4242/cancel",
        };

        var service = new SessionService();
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}