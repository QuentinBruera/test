using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetSale(int id);
        Task<IEnumerable<Sale>> GetSales();
        Task CreateSale(Sale sale);
        Task UpdateSale(Sale sale);
        Task DeleteSaleWithArticles(int id);
    }
}
