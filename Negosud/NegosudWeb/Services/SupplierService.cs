using NegosudModel.Dto;
using Newtonsoft.Json;

namespace NegosudWeb.Services
{
    public class SupplierService
    {
        private readonly HttpClient _httpClient;

        public SupplierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var response = await _httpClient.GetAsync("api/suppliers", cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erreur Http : {response.StatusCode}");
            }

            var resultat = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<SupplierDto>>(resultat) ?? Enumerable.Empty<SupplierDto>();
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int supplierId)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var response = await _httpClient.GetAsync($"api/suppliers/{supplierId}", cts.Token);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SupplierDto>(json);
        }
    }
}
