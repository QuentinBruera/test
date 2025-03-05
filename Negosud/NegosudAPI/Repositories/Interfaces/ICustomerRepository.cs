using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomer(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task CreateCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int id);
    }
}
