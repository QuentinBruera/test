using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class ArticleOrderRepository : IArticleOrderRepository
    {
        private readonly NegosudContext _context;

        public ArticleOrderRepository(NegosudContext context)
        {
            _context = context;
        }

        public NegosudContext GetContext()
        {
            return _context;
        }


        public async Task<ArticleOrder?> GetArticleOrder(int id)
        {
            return await _context.ArticleOrders
                .Include(a => a.Article)
                .Include(a => a.Order)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<ArticleOrder>> GetArticleOrdersByOrderId(int orderId)
        {
            return await _context.ArticleOrders
                .Include(a => a.Article)
                .Include(a => a.Order)
                .Where(a => a.OrderId == orderId)
                .ToListAsync();
        }

        public async Task CreateArticleOrder(ArticleOrder articleOrder)
        {
            if (articleOrder.Order == null) throw new ArgumentNullException(nameof(articleOrder.Order), "Order cannot be null.");
            if (articleOrder.Article == null) throw new ArgumentNullException(nameof(articleOrder.Article), "Article cannot be null.");

            _context.Attach(articleOrder.Order);
            _context.Attach(articleOrder.Article);

            _context.ArticleOrders.Add(articleOrder);
            await _context.SaveChangesAsync();
        }
    }
}
