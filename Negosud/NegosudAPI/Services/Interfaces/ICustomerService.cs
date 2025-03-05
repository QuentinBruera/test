using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerEntity(int id);
        Task<CustomerDto?> GetCustomerDto(int id);
        Task<IEnumerable<CustomerDto>> GetCustomers();
        Task<int> CreateCustomer(CreateUpdateCustomerRequest request);
        Task<bool> UpdateCustomer(int id, CreateUpdateCustomerRequest request);
        Task<bool> DeleteCustomer(int id);
    }
}
