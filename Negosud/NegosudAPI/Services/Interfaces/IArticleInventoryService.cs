using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Interfaces
{
    public interface IArticleInventoryService
    {
        Task CreateArticleInventory(ArticleInventory articleInventory);
        Task<List<ArticleInventory>> GetArticleInventoriesByInventoryId(int inventoryId);
        Task<List<ArticleInventoryDto>> GetArticleInventoriesDtoByInventoryId(int inventoryId);
        Task<List<ArticleInventoryDto>> GetArticleInventoriesDtoByArticleId(int articleId);
    }
}
