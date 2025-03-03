using Model.Dto;
using System.Net.Http;
using System.Net.Http.Json;

namespace Client.Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartments()
        {
            try
            {
                IEnumerable<DepartmentDto>? response = await _httpClient.GetFromJsonAsync<IEnumerable<DepartmentDto>>("api/departments");
                return response ?? Enumerable.Empty<DepartmentDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel API pour récupérer les départements : {ex.Message}");
                throw;
            }
        }

        public async Task<DepartmentDto?> GetDepartment(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DepartmentDto>($"api/departments/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'appel API pour récupérer le département {id} : {ex.Message}");
                throw;
            }
        }
    }
}