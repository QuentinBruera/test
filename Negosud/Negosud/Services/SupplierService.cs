using System.Net.Http;
using System.Net.Http.Json;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace Negosud.Services
{
    public class SupplierService : ApiService
    {
        public async Task<IEnumerable<SupplierDto>> GetSuppliersAsync()
        {
            try
            {
                IEnumerable<SupplierDto>? suppliers = await _httpClient.GetFromJsonAsync<IEnumerable<SupplierDto>>("api/suppliers");
                return suppliers ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des fournisseurs : {ex.Message}");
                return [];
            }
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int supplierId)
        {
            try
            {
                var supplier = await _httpClient.GetFromJsonAsync<SupplierDto>($"api/suppliers/{supplierId}");
                return supplier;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du fournisseur (ID: {supplierId}): {ex.Message}");
                return null;
            }
        }


        public async Task<int> CreateSupplierAsync(CreateUpdateSupplierRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/suppliers", request);

                if (response.IsSuccessStatusCode)
                {
                    int? supplierId = await response.Content.ReadFromJsonAsync<int>();
                    return supplierId ?? -1;
                }

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while creating supplier: {ex.Message}");
                return -1;
            }
        }

        public async Task<bool> UpdateSupplierAsync(int supplierId, CreateUpdateSupplierRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/suppliers/{supplierId}", request);

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while updating supplier with ID {supplierId}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSupplierAsync(int supplierId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/suppliers/{supplierId}");

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while deleting supplier with ID {supplierId}: {ex.Message}");
                return false;
            }
        }
    }
}
