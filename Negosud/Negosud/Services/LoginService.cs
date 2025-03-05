using System.Net.Http;
using System.Net.Http.Json;
using NegosudModel.Request;

namespace Negosud.Services
{
    public class LoginService : ApiService
    {
        private const string DefaultEmailAdmin = "admin@gmail.com";
        private const string DefaultPasswordAdmin = "Admin1!";

        private const string DefaultEmailService = "service@gmail.com";
        private const string DefaultPasswordService = "Service1!";

        public async Task<string?> LoginAsync(LoginRequest request, bool useCookies, bool useSessionCookies)
        {
            try
            {
                string url = $"login?useCookies={useCookies.ToString().ToLower()}&useSessionCookies={useSessionCookies.ToString().ToLower()}";

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, request);

                if (response.IsSuccessStatusCode)
                {
                    string? token = await response.Content.ReadAsStringAsync();
                    return token;
                }

                Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception lors de la connexion : {ex.Message}");
                return null;
            }
        }

        public async Task<string?> LoginAsAdmin(bool useCookies, bool useSessionCookies)
        {
            var request = new LoginRequest
            {
                Email = DefaultEmailAdmin,
                Password = DefaultPasswordAdmin
            };

            return await LoginAsync(request, useCookies, useSessionCookies);
        }

        public async Task<string?> LoginAsService(bool useCookies, bool useSessionCookies)
        {
            var request = new LoginRequest
            {
                Email = DefaultEmailService,
                Password = DefaultPasswordService
            };

            return await LoginAsync(request, useCookies, useSessionCookies);
        }
    }
}