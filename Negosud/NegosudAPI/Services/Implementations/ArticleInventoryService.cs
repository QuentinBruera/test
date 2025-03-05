using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class ArticleInventoryService : IArticleInventoryService
    {
        private readonly IArticleInventoryRepository _articleInventoryRepository;

        public ArticleInventoryService(IArticleInventoryRepository articleInventoryRepository)
        {
            _articleInventoryRepository = articleInventoryRepository;
        }

        public async Task<List<ArticleInventory>> GetArticleInventoriesByInventoryId(int inventoryId)
        {
            List<ArticleInventory> articleInventories = await _articleInventoryRepository.GetArticleInventoriesByInventoryId(inventoryId);
            return articleInventories;
        }

        public async Task<List<ArticleInventoryDto>> GetArticleInventoriesDtoByInventoryId(int inventoryId)
        {
            List<ArticleInventory> articleInventories = await GetArticleInventoriesByInventoryId(inventoryId);
            return articleInventories.Select(articleInventory => articleInventory.ToDto()).ToList();
        }

        public async Task CreateArticleInventory(ArticleInventory articleInventory)
        {
            if (articleInventory == null) throw new ArgumentNullException(nameof(articleInventory), "ArticleInventory cannot be null.");
            await _articleInventoryRepository.CreateArticleInventory(articleInventory);
        }

        public async Task<List<ArticleInventoryDto>> GetArticleInventoriesDtoByArticleId(int articleId)
        {
            List<ArticleInventory> articleInventories = await _articleInventoryRepository.GetArticleInventoriesByArticleId(articleId);
            return articleInventories.Select(articleInventory => articleInventory.ToDto()).ToList();
        }
    }
}
