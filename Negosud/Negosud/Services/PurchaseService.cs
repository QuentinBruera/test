using System.Net.Http;
using System.Net.Http.Json;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace Negosud.Services
{
    public class PurchaseService : ApiService
    {
        public async Task<IEnumerable<PurchaseDto>> GetPurchases()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/purchases");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<PurchaseDto>? purchases = await response.Content.ReadFromJsonAsync<IEnumerable<PurchaseDto>>();
                    return purchases ?? [];
                }

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while retrieving purchases : {ex.Message}");
                return [];
            }
        }

        public async Task<IEnumerable<PurchaseDto>> GetReceptions()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/purchases/receptions");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<PurchaseDto>? receptions = await response.Content.ReadFromJsonAsync<IEnumerable<PurchaseDto>>();
                    return receptions ?? [];
                }

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while retrieving receptions : {ex.Message}");
                return [];
            }
        }

        public async Task<PurchaseDto?> GetPurchaseByIdAsync(int purchaseId)
        {
            try
            {
                PurchaseDto? purchase = await _httpClient.GetFromJsonAsync<PurchaseDto>($"api/purchases/{purchaseId}");
                return purchase;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving purchase {purchaseId} : {ex.Message}");
                return null;
            }
        }

        public async Task<int> CreatePurchaseWithArticlesAsync(CreatePurchaseRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/purchases", request);

                if (response.IsSuccessStatusCode)
                {
                    int? purchaseId = await response.Content.ReadFromJsonAsync<int>();
                    return purchaseId ?? -1;
                }

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while creating purchase : {ex.Message}");
                return -1;
            }
        }

        public async Task<int> CreatePurchaseRestockingAsync(int articleId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync($"api/purchases/restocking/{articleId}", null);

                if (response.IsSuccessStatusCode)
                {
                    int? purchaseId = await response.Content.ReadFromJsonAsync<int>();
                    return purchaseId ?? -1;
                }

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while creating restocking purchase for article {articleId}: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> CancelPurchase(int purchaseId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsync($"api/purchases/cancel/{purchaseId}", null);

                if (response.IsSuccessStatusCode) return true;
                
                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while canceling purchase {purchaseId}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ReceivePurchase(int purchaseId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/purchases/receive/{purchaseId}", new { });

                if (response.IsSuccessStatusCode) return true;
                

                Console.WriteLine($"Error API : {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while receving purchase {purchaseId}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeletePurchase(int purchaseId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/purchases/{purchaseId}");

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while deleting purchase {purchaseId}: {ex.Message}");
                return false;
            }
        }
    }
}
