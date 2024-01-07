using Services.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.Bridge
{
    public class PaymentWebApiService : IPaymentDetails
    {
        private readonly HttpClient _httpClient;

        public PaymentWebApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PaymentDetailModel>> GetPaymentDetails()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/payments");
                response.EnsureSuccessStatusCode();
                var paymentDetails = await response.Content.ReadFromJsonAsync<IEnumerable<PaymentDetailModel>>();
                return paymentDetails;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to propagate it
            }

        }



        public async Task<PaymentDetailModel> PostPaymentDetail(PaymentDetailModel paymentDetail)
        {
            var response = await _httpClient.PostAsJsonAsync("addPayment", paymentDetail); // Updated endpoint
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PaymentDetailModel>();
        }

        public async Task<PaymentDetailModel> GetPaymentDetail(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<PaymentDetailModel>($"getPayment/{id}"); // Updated endpoint
            return response;
        }

        public async Task DeletePaymentDetail(int id)
        {
            var response = await _httpClient.DeleteAsync($"removePayment/{id}"); // Updated endpoint
            response.EnsureSuccessStatusCode();
        }

        public async Task PutPaymentDetail(int id, PaymentDetailModel paymentDetail)
        {
            var response = await _httpClient.PutAsJsonAsync($"updatePayment/{id}", paymentDetail); // Updated endpoint
            response.EnsureSuccessStatusCode();
        }
    }
}
