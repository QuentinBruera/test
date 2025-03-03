using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Model.Request;

namespace Client.Services
{
    public class LoginApiService
    {
        private const string DefaultEmailAdmin = "admin@gmail.com";
        private const string DefaultPasswordAdmin = "Admin1!";

        // Utiliser l'instance singleton de ApiService
        private ApiService _apiService = ApiService.Instance;

        public ApiService ApiService
        {
            get => _apiService;
        }

        public async Task<bool> LoginAsync(LoginRequest request, bool useCookies, bool useSessionCookies)
        {
            try
            {
                string url = $"login?useCookies={useCookies.ToString().ToLower()}&useSessionCookies={useSessionCookies.ToString().ToLower()}";

                HttpResponseMessage response = await _apiService.HttpClient.PostAsJsonAsync(url, request);

                Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception lors de la connexion : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LoginAsAdmin(bool useCookies, bool useSessionCookies)
        {
            var request = new LoginRequest
            {
                Email = DefaultEmailAdmin,
                Password = DefaultPasswordAdmin
            };

            return await LoginAsync(request, useCookies, useSessionCookies);
        }
    }
}