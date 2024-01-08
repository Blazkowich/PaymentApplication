using Services.Models;
using System.Net.Http.Json;

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
                return paymentDetails!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<PaymentDetailModel> GetPaymentDetail(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<PaymentDetailModel>($"getPayment/{id}");
            return response!;
        }
        public async Task<PaymentDetailModel> PostPaymentDetail(PaymentDetailModel paymentDetail)
        {
            var response = await _httpClient.PostAsJsonAsync("api/payments", paymentDetail);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorMessage}");
            }

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<PaymentDetailModel>();

            return result;
        }
        public async Task PutPaymentDetail(int id, PaymentDetailModel paymentDetail)
        {
            var response = await _httpClient.PutAsJsonAsync($"updatePayment/{id}", paymentDetail);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePaymentDetail(int id)
        {
            var response = await _httpClient.DeleteAsync($"removePayment/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
