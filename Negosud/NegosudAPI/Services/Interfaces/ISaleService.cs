using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDto?> GetSaleDto(int id);
        Task<IEnumerable<SaleDto>> GetSales();
        Task<int> CreateSaleWithArticles(CreateSaleRequest request);
        Task<Sale> CreateSale(SaleDto saleDto);
        Task<Sale> CreateSale(CreateSaleRequest request);
        Task<bool> CancelSale(int id);
        Task<bool> ReceiveSale(int id);
        Task<bool> DeleteSale(int id);
    }
}
