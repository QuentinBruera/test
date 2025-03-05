using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<Supplier?> GetSupplierEntity(int id);
        Task<SupplierDto?> GetSupplierDto(int id);
        Task<IEnumerable<SupplierDto>> GetSuppliers();
        Task<int> CreateSupplier(CreateUpdateSupplierRequest request);
        Task<bool> UpdateSupplier(int id, CreateUpdateSupplierRequest request);
        Task<bool> DeleteSupplier(int id);
    }
}
