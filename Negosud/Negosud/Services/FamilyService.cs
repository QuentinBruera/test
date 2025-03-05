using NegosudModel.Dto;
using System.Net.Http.Json;

namespace Negosud.Services
{
    public class FamilyService : ApiService
    {
        public async Task<FamilyDto?> GetFamilyByIdAsync(int familyId)
        {
            try
            {
                var family = await _httpClient.GetFromJsonAsync<FamilyDto>($"api/families/{familyId}");
                return family;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération de la famille (ID: {familyId}): {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<FamilyDto>> GetFamiliesAsync()
        {
            try
            {
                var families = await _httpClient.GetFromJsonAsync<IEnumerable<FamilyDto>>("api/families");
                return families ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des familles : {ex.Message}");
                return [];
            }
        }
    }
}
