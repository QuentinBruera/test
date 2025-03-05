using NegosudModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.Services
{
    public class ArticleInventoryService : ApiService
    {
        public async Task<IEnumerable<ArticleInventoryDto>> GetArticleInventoriesByInventoryId(int inventoryId)
        {
            try
            {
                IEnumerable<ArticleInventoryDto>? articleInventories = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleInventoryDto>>($"api/articleInventories?inventoryId={inventoryId}");
                return articleInventories ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving article inventories for inventory {inventoryId}: {ex.Message}");
                return [];
            }
        }
    }
}
