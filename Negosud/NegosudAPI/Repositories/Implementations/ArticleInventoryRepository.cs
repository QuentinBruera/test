using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class ArticleInventoryRepository : IArticleInventoryRepository
    {
        private readonly NegosudContext _context;

        public ArticleInventoryRepository(NegosudContext context)
        {
            _context = context;
        }

        public NegosudContext GetContext()
        {
            return _context;
        }

        public async Task<List<ArticleInventory>> GetArticleInventoriesByInventoryId(int inventoryId)
        {
            return await _context.ArticleInventories
                .Include(a => a.Article)
                .Include(a => a.Inventory)
                .Where(a => a.InventoryId == inventoryId)
                .ToListAsync();
        }

        public async Task CreateArticleInventory(ArticleInventory articleInventory)
        {
            if (articleInventory.Inventory == null) throw new ArgumentNullException(nameof(articleInventory.Inventory), "Inventory cannot be null.");
            if (articleInventory.Article == null) throw new ArgumentNullException(nameof(articleInventory.Article), "Article cannot be null.");

            _context.Attach(articleInventory.Inventory);
            _context.Attach(articleInventory.Article);

            _context.ArticleInventories.Add(articleInventory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ArticleInventory>> GetArticleInventoriesByArticleId(int articleId)
        {
            return await _context.ArticleInventories
                .Include(a => a.Article)
                .Include(a => a.Inventory)
                .Where(a => a.ArticleId == articleId)
                .ToListAsync();
        }
    }
}
