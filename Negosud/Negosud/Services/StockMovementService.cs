using NegosudModel.Dto;
using NegosudModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.Services
{
    public class StockMovementService : ApiService
    {
        public async Task<StockMovementDto> PostStockMovement(StockMovementDto request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/stockMovement", request);
                if (response.IsSuccessStatusCode)
                {
                    StockMovementDto? stockMovement = await response.Content.ReadFromJsonAsync<StockMovementDto>();
                    return stockMovement ?? new StockMovementDto();
                }
                return new StockMovementDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while posting stock movement: {ex.Message}");
                return new StockMovementDto();
            }
        }

        public async Task<IEnumerable<StockMovementDto>> GetStockMovementsByArticleId(int articleId)
        {
            try
            {
                // /api/stockMovement?articleId=1
                HttpResponseMessage response = await _httpClient.GetAsync($"api/stockMovement?articleId={articleId}");
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<StockMovementDto>? stockMovements = await response.Content.ReadFromJsonAsync<IEnumerable<StockMovementDto>>();
                    return stockMovements ?? new List<StockMovementDto>();
                }
                return new List<StockMovementDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while getting stock movements by article id: {ex.Message}");
                return new List<StockMovementDto>();
            }
        }
    }
}
