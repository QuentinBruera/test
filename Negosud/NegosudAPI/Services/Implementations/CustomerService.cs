using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer?> GetCustomerEntity(int id)
        {
            return await _customerRepository.GetCustomer(id);
        }

        public async Task<CustomerDto?> GetCustomerDto(int id)
        {
            Customer? customer = await GetCustomerEntity(id);

            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                FirstName = customer.FirstName,
                DateOfBirth = customer.DateOfBirth,
                Address = customer.Address,
                City = customer.City,
                ZipCode = customer.ZipCode,
                LandlineNumber = customer.LandlineNumber,
                CellPhoneNumber = customer.CellPhoneNumber
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetCustomers();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                FirstName= c.FirstName,
                DateOfBirth = c.DateOfBirth,
                Address = c.Address,
                City = c.City,
                ZipCode = c.ZipCode,
                LandlineNumber = c.LandlineNumber,
                CellPhoneNumber = c.CellPhoneNumber
            });
        }

        public async Task<int> CreateCustomer(CreateUpdateCustomerRequest request)
        {
            Customer customer = new()
            {
                Name = request.Name,
                FirstName = request.FirstName,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                City = request.City,
                ZipCode = request.ZipCode,
                LandlineNumber = request.LandlineNumber,
                CellPhoneNumber = request.CellPhoneNumber
            };

            await _customerRepository.CreateCustomer(customer);
            return customer.Id;
        }

        public async Task<bool> UpdateCustomer(int id, CreateUpdateCustomerRequest request)
        {
            Customer? customer = await _customerRepository.GetCustomer(id);
            if (customer == null) return false;

            customer.Name = request.Name;
            customer.FirstName = request.FirstName;
            customer.DateOfBirth = request.DateOfBirth;
            customer.Address = request.Address;
            customer.City = request.City;
            customer.ZipCode = request.ZipCode;
            customer.LandlineNumber = request.LandlineNumber;
            customer.CellPhoneNumber = request.CellPhoneNumber;

            await _customerRepository.UpdateCustomer(customer);

            return true;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            Customer? customer = await _customerRepository.GetCustomer(id);
            if (customer == null) return false;

            await _customerRepository.DeleteCustomer(id);
            return true;
        }
    }
}
