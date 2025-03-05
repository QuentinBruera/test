using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<PurchaseDto?> GetPurchaseDto(int id);
        Task<IEnumerable<PurchaseDto>> GetPurchases();
        Task<IEnumerable<PurchaseDto>> GetReceptions();
        Task<int> CreatePurchaseWithArticles(CreatePurchaseRequest request);
        Task<Purchase> CreatePurchase(PurchaseDto purchaseDto);
        Task<Purchase> CreatePurchase(CreatePurchaseRequest request);
        Task<int> CreatePurchaseRestocking(int articleId);
        Task<bool> CancelPurchase(int id);
        Task<bool> ReceivePurchase(int id);
        Task<bool> DeletePurchase(int purchaseId);
    }
}
