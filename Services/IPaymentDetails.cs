using Services.Models;

namespace Services
{
    public interface IPaymentDetails
    {
        Task<IEnumerable<PaymentDetailModel>> GetPaymentDetails();

        Task<PaymentDetailModel> GetPaymentDetail(int id);

        Task PutPaymentDetail(int id, PaymentDetailModel paymentDetail);

        Task<PaymentDetailModel> PostPaymentDetail(PaymentDetailModel paymentDetail);

        Task DeletePaymentDetail(int id);
    }
}