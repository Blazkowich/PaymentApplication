using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentApiController : ControllerBase
    {
        private readonly IPaymentDetails _payment;

        public PaymentApiController(IPaymentDetails payment)
        {
            _payment = payment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetailModel>>> GetPaymentDetails()
        {
            var payment = await _payment.GetPaymentDetails();

            return Ok(payment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetailModel>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _payment.GetPaymentDetail(id);

            if (paymentDetail == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }

            return Ok(paymentDetail);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetailModel>> PostPaymentDetail(PaymentDetailModel paymentDetail)
        {
            var payment = await _payment.PostPaymentDetail(paymentDetail);

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PaymentDetailId }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetailModel paymentDetail)
        {
            await _payment.PutPaymentDetail(id, paymentDetail);
            return Ok(paymentDetail);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            await _payment.DeletePaymentDetail(id);
            return Ok($"Payment ID:{id} Removed");
        }
    }
}
