using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Interfaces
{
    public interface IStockMovementService
    {
        Task<List<StockMovementDto>> GetStockMovementsByArticleId(int articleId);
        Task<StockMovementDto> CreateStockMovement(StockMovementDto stockMovementDto);
    }
}
