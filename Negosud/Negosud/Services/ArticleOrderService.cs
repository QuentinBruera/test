using System.Net.Http.Json;
using NegosudModel.Dto;

namespace Negosud.Services
{
    public class ArticleOrderService : ApiService
    {
        public async Task<IEnumerable<ArticleOrderDto>> GetArticleOrdersByPurchaseId(int purchaseId)
        {
            try
            {
                IEnumerable<ArticleOrderDto>? articleOrders = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleOrderDto>>($"api/articleOrders?purchaseId={purchaseId}");
                return articleOrders ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving article orders for purchase {purchaseId}: {ex.Message}");
                return [];
            }
        }
    }
}
