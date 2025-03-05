using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IArticleInventoryRepository
    {
        Task<List<ArticleInventory>> GetArticleInventoriesByInventoryId(int inventoryId);
        Task CreateArticleInventory(ArticleInventory articleInventory);
        Task<List<ArticleInventory>> GetArticleInventoriesByArticleId(int articleId);
    }
}
