using System.Net.Http.Json;
using NegosudModel.Dto;

namespace Negosud.Services
{
    public class StatusService : ApiService
    {
        public async Task<StatusDto?> GetStatusByIdAsync(int statusId)
        {
            try
            {
                var status = await _httpClient.GetFromJsonAsync<StatusDto>($"api/status/{statusId}");
                return status;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving status {statusId} : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateStatusAsync(int entityId, int newStatusId)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/status/update", new
                {
                    EntityId = entityId,
                    StatusId = newStatusId
                });

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Status successfully updated for entity {entityId}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error updating status for entity {entityId} : {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating status for entity {entityId} : {ex.Message}");
                return false;
            }
        }
    }
}
