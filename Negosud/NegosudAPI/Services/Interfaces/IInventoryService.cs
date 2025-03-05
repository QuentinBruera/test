using NegosudModel.Dto;
using NegosudModel.Request;

namespace NegosudAPI.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<InventoryDto?> GetInventoryDto(int id);
        Task<IEnumerable<InventoryDto>> GetInventories();
        Task<int> CreateInventoryWithArticles(CreateInventoryRequest request);
        Task<bool> DeleteInventory(int inventoryId);
    }
}
