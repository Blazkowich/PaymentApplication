using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Database
{
    public class PaymentsDetailDbService : IPaymentDetails
    {
        private readonly PaymentDbContext _context;

        public PaymentsDetailDbService(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDetailModel>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        public async Task<PaymentDetailModel> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return null;
            }

            return paymentDetail;
        }

        public async Task<PaymentDetailModel> PostPaymentDetail(PaymentDetailModel paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return paymentDetail;
        }

        public async Task PutPaymentDetail(int id, PaymentDetailModel paymentDetail)
        {
            if (id != paymentDetail.PaymentDetailId)
            {
                throw new ArgumentException("Bad Request", nameof(id));
            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    throw new ArgumentException($"Id {id} does not exist.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                throw new ArgumentNullException(nameof(paymentDetail));
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailId == id);
        }
    }
}