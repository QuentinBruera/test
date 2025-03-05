using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class SaleRepository : ISaleRepository
    {
        private readonly NegosudContext _context;

        public SaleRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetSale(int id)
        {
            return await _context.Sales
                .Include(s => s.Status)
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sale>> GetSales()
        {
            return await _context.Sales
                .Include(s => s.Status)
                .Include(s => s.Customer)
                .OrderBy(s => s.Id)
                .ToListAsync();
        }

        public async Task CreateSale(Sale sale)
        {
            if (sale.Customer == null) throw new ArgumentNullException(nameof(sale.Customer), "Customer cannot be null.");
            if (sale.Status == null) throw new ArgumentNullException(nameof(sale.Status), "Status cannot be null.");

            _context.Attach(sale.Customer);
            _context.Attach(sale.Status);

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSale(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaleWithArticles(int id)
        {
            var articleOrders = _context.ArticleOrders.Where(ao => ao.OrderId == id);
            _context.ArticleOrders.RemoveRange(articleOrders);

            Sale? sale = await _context.Sales.FindAsync(id);
            if (sale != null) _context.Sales.Remove(sale);

            await _context.SaveChangesAsync();
        }
    }
}
