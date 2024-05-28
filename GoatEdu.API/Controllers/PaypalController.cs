using Microsoft.AspNetCore.Mvc;
using PaypalCheckoutExample.Clients;
using System.Threading.Tasks;

namespace GoatEdu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaypalController : ControllerBase
    {
        private readonly PaypalClient _paypalClient;

        public PaypalController(PaypalClient paypalClient)
        {
            _paypalClient = paypalClient;
        }

        [HttpPost("Order")]
        public async Task<IActionResult> Order(CancellationToken cancellationToken)
        {
            try
            {
                // set the transaction price and currency
                var price = "100.00";
                var currency = "USD";

                // "reference" is the transaction key
                var reference = "INV001";

                var response = await _paypalClient.CreateOrder(price, currency, reference);

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        [HttpPost("Capture")]
        public async Task<IActionResult> Capture([FromBody] string orderId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);

                var reference = response.purchase_units[0].reference_id;

                // Put your logic to save the transaction here
                // You can use the "reference" variable as a transaction key

                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }
    }
}