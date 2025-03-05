using System.Net.Http;
using System.Net.Http.Json;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace Negosud.Services
{
    public class CustomerService : ApiService
    {
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/customers");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<CustomerDto>? customers = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>();
                    return customers ?? [];
                }

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while retrieving customers: {ex.Message}");
                return [];
            }
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                CustomerDto? customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{customerId}");
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer with ID {customerId}: {ex.Message}");
                return null;
            }
        }

        public async Task<int> CreateCustomerAsync(CreateUpdateCustomerRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/customers", request);

                if (response.IsSuccessStatusCode)
                {
                    int? customerId = await response.Content.ReadFromJsonAsync<int>();
                    return customerId ?? -1;
                }

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while creating customer: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> UpdateCustomerAsync(int customerId, CreateUpdateCustomerRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/customers/{customerId}", request);

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while updating customer with ID {customerId}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/customers/{customerId}");

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while deleting customer with ID {customerId}: {ex.Message}");
                return false;
            }
        }
    }
}