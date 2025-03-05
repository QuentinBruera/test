using NegosudModel.Dto;
using NegosudModel.Request;
using System.Net.Http;
using System.Net.Http.Json;

namespace Negosud.Services
{
    public class SaleService : ApiService
    {
        public async Task<IEnumerable<SaleDto>> GetSales()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/sales");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<SaleDto>? sales = await response.Content.ReadFromJsonAsync<IEnumerable<SaleDto>>();
                    return sales ?? [];
                }

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while retrieving sales : {ex.Message}");
                return [];
            }
        }
        public async Task<SaleDto?> GetSaleByIdAsync(int saleId)
        {
            try
            {
                SaleDto? sale = await _httpClient.GetFromJsonAsync<SaleDto>($"api/sales/{saleId}");
                return sale;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving sale {saleId} : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CancelSale(int saleId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsync($"api/sales/cancel/{saleId}", null);

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while canceling sale {saleId}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ReceiveSale(int saleId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/sales/receive/{saleId}", new { });

                if (response.IsSuccessStatusCode) return true;


                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while receving sale {saleId}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSale(int saleId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/sales/{saleId}");

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while deleting sale {saleId}: {ex.Message}");
                return false;
            }
        }

        public async Task<int> CreateSaleWithArticlesAsync(CreateSaleRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/sales", request);

                if (response.IsSuccessStatusCode)
                {
                    int? saleId = await response.Content.ReadFromJsonAsync<int>();
                    return saleId ?? -1;
                }

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while creating sale : {ex.Message}");
                return -1;
            }
        }
    }
}
