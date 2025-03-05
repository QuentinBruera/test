using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly NegosudContext _context;

        public StockMovementRepository(NegosudContext context)
        {
            _context = context;
        }
        public async Task<List<StockMovement>> GetStockMovementsByArticleId(int articleId)
        {
            return await _context.StockMovements
                .Where(sm => sm.ArticleId == articleId)
                .ToListAsync();
        }

        public async Task CreateStockMovement(StockMovement stockMovement)
        {
            _context.Attach(stockMovement.Reasons);
            _context.Attach(stockMovement.Article);

            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();
        }
    }
}
