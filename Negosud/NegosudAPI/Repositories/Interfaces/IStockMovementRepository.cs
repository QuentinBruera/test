using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<List<StockMovement>> GetStockMovementsByArticleId(int articleId);
        Task CreateStockMovement(StockMovement stockMovement);
    }
}
