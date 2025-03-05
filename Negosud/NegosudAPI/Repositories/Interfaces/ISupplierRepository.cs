using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetSupplier(int id);
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task CreateSupplier(Supplier supplier);
        Task UpdateSupplier(Supplier supplier);
        Task DeleteSupplier(int id);
    }
}
