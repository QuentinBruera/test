using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier?> GetSupplierEntity(int id)
        {
            return await _supplierRepository.GetSupplier(id);
        }

        public async Task<SupplierDto?> GetSupplierDto(int id)
        {
            Supplier? supplier = await GetSupplierEntity(id);

            if (supplier == null) return null;

            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                City = supplier.City,
                ZipCode = supplier.ZipCode,
                LandlineNumber = supplier.LandlineNumber,
                CellPhoneNumber = supplier.CellPhoneNumber
            };
        }

        public async Task<IEnumerable<SupplierDto>> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = await _supplierRepository.GetSuppliers();

            return suppliers.Select(s => new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                City = s.City,
                ZipCode = s.ZipCode,
                LandlineNumber = s.LandlineNumber,
                CellPhoneNumber = s.CellPhoneNumber
            });
        }

            public async Task<int> CreateSupplier(CreateUpdateSupplierRequest request)
        {
            Supplier supplier = new()
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                ZipCode = request.ZipCode,
                LandlineNumber = request.LandlineNumber,
                CellPhoneNumber = request.CellPhoneNumber
            };

            await _supplierRepository.CreateSupplier(supplier);
            return supplier.Id;
        }
        public async Task<bool> UpdateSupplier(int id, CreateUpdateSupplierRequest request)
        {
            Supplier? supplier = await _supplierRepository.GetSupplier(id);
            if (supplier == null) return false;

            supplier.Name = request.Name;
            supplier.Address = request.Address;
            supplier.City = request.City;
            supplier.ZipCode = request.ZipCode;
            supplier.LandlineNumber = request.LandlineNumber;
            supplier.CellPhoneNumber = request.CellPhoneNumber;

            await _supplierRepository.UpdateSupplier(supplier);

            return true;
        }

        public async Task<bool> DeleteSupplier(int id)
        {
            Supplier? supplier = await _supplierRepository.GetSupplier(id);
            if (supplier == null) return false;

            await _supplierRepository.DeleteSupplier(id);
            return true;
        }
    }
    }
