using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> GetPurchase(int id);
        Task<IEnumerable<Purchase>> GetPurchases();
        Task<IEnumerable<Purchase>> GetReceptions();
        Task CreatePurchase(Purchase purchase);
        Task UpdatePurchase(Purchase purchase);
        Task DeletePurchaseWithArticles(int purchaseId);
    }
}
