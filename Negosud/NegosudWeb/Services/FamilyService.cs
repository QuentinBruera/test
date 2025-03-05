using NegosudModel.Dto;
using Newtonsoft.Json;

namespace NegosudWeb.Services
{
    public class FamilyService
    {
        private readonly HttpClient _httpClient;

        public FamilyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<FamilyDto>> GetFamiliesAsync()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var response = await _httpClient.GetAsync("api/families", cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erreur Http : {response.StatusCode}");
            }

            var resultat = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<FamilyDto>>(resultat) ?? Enumerable.Empty<FamilyDto>();
        }
    }
}
