using NegosudModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Negosud.Services
{
    internal class ReasonService : ApiService
    {
        public async Task<IEnumerable<ReasonDto>> GetReasonsAsync()
        {
            try
            {
                IEnumerable<ReasonDto>? reasons = await _httpClient.GetFromJsonAsync<IEnumerable<ReasonDto>>("api/reasons");
                return reasons ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving reasons: {ex.Message}");
                return [];
            }
        }
    }
}
