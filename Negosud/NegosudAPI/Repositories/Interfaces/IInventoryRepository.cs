using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetInventory(int id);
        Task<IEnumerable<Inventory>> GetInventories();
        Task CreateInventory(Inventory inventory);
        Task DeleteInventoryWithArticles(int inventoryId);
    }
}
