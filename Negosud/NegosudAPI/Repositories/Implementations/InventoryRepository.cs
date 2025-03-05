using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {

        private readonly NegosudContext _context;

        public InventoryRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Inventory?> GetInventory(int id)
        {
            return await _context.Inventories
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetInventories()
        {
            return await _context.Inventories
                .OrderBy(i => i.Id)
                .ToListAsync();
        }

        public async Task CreateInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInventoryWithArticles(int inventoryId)
        {
            var articleInventories = _context.ArticleInventories.Where(ai => ai.InventoryId == inventoryId);
            _context.ArticleInventories.RemoveRange(articleInventories);

            Inventory? inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory != null) _context.Inventories.Remove(inventory);

            await _context.SaveChangesAsync();
        }
    }
}
