using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly NegosudContext _context;

        public PurchaseRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Purchase?> GetPurchase(int id)
        {
            return await _context.Purchases
                .Include(p => p.Status)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Purchase>> GetPurchases()
        {
            return await _context.Purchases
                .Include(p => p.Status)
                .Include(p => p.Supplier)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Purchase>> GetReceptions()
        {
            return await _context.Purchases
                .Include(p => p.Status)
                .Include(p => p.Supplier)
                .Where(p => p.Status != null && p.Status.Id == 9)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task CreatePurchase(Purchase purchase)
        {
            if (purchase.Supplier == null) throw new ArgumentNullException(nameof(purchase.Supplier), "Supplier cannot be null.");
            if (purchase.Status == null) throw new ArgumentNullException(nameof(purchase.Status), "Status cannot be null.");

            _context.Attach(purchase.Supplier);
            _context.Attach(purchase.Status);

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePurchase(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePurchaseWithArticles(int purchaseId)
        {
            var articleOrders = _context.ArticleOrders.Where(ao => ao.OrderId == purchaseId);
            _context.ArticleOrders.RemoveRange(articleOrders);

            Purchase? purchase = await _context.Purchases.FindAsync(purchaseId);
            if (purchase != null) _context.Purchases.Remove(purchase);
            
            await _context.SaveChangesAsync();
        }
    }
}
