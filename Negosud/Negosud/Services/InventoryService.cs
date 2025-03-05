using NegosudModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NegosudModel.Dto;

namespace Negosud.Services
{
    public class InventoryService : ApiService
    {
        public async Task<IEnumerable<InventoryDto>> GetInventoriesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("api/inventories");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<InventoryDto>? inventories = await response.Content.ReadFromJsonAsync<IEnumerable<InventoryDto>>();
                    return inventories ?? [];
                }

                Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                return [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching inventories : {ex.Message}");
                return [];
            }
        }

        public async Task<bool> CreateInventoryAsync(CreateInventoryRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/inventories", request);

                if (response.IsSuccessStatusCode) return true;
                
                Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating inventory : {ex.Message}");
                return false;
            }
        }

        public async Task<InventoryDto?> GetInventoryByIdAsync(int inventoryId)
        {
            try
            {
                InventoryDto? inventory = await _httpClient.GetFromJsonAsync<InventoryDto>($"api/inventories/{inventoryId}");
                return inventory;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving inventory {inventoryId} : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteInventory(int inventoryId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/inventories/{inventoryId}");

                if (response.IsSuccessStatusCode) return true;

                Console.WriteLine($"Error API: {response.StatusCode} - {response.ReasonPhrase}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while deleting inventory {inventoryId}: {ex.Message}");
                return false;
            }
        }
    }
}
