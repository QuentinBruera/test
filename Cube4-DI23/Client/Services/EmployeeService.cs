using Model.Dto;
using System.Net.Http;
using System.Net.Http.Json;

namespace Client.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            try
            {
                IEnumerable<EmployeeDto>? response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>("api/employees");
                return response ?? Enumerable.Empty<EmployeeDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel API pour récupérer les employés : {ex.Message}");
                throw;
            }
        }

        public async Task<EmployeeDto?> GetEmployee(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<EmployeeDto>($"api/employees/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel API pour récupérer l'employé {id} : {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployees(string searchTerm)
        {
            try
            {
                IEnumerable<EmployeeDto>? response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>($"api/employees/search?term={searchTerm}");
                return response ?? Enumerable.Empty<EmployeeDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la recherche d'employés : {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesBySite(int siteId)
        {
            try
            {
                IEnumerable<EmployeeDto>? response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>($"api/employees/site/{siteId}");
                return response ?? Enumerable.Empty<EmployeeDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des employés par site : {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesByDepartment(int departmentId)
        {
            try
            {
                IEnumerable<EmployeeDto>? response = await _httpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>($"api/employees/department/{departmentId}");
                return response ?? Enumerable.Empty<EmployeeDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des employés par département : {ex.Message}");
                throw;
            }
        }
    }
}