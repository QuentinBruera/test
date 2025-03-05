using NegosudModel.Dto;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace NegosudWeb.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public HttpClient HttpClient => _httpClient;
        private CookieContainer _cookieContainer = new();

        public bool IsLoggedIn { get; private set; } = false;

        public ApiService(string baseAddress)
        {
            var handler = new HttpClientHandler { CookieContainer = _cookieContainer };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };
        }

        public async Task<bool> Login(string username, string password)
        {
            if (IsLoggedIn) // On évite de relancer la connexion si déjà connecté
            {
                return true;
            }

            string route = "login?useCookies=true&useSessionCookies=true";
            var jsonString = $"{{ \"email\": \"{username}\", \"password\": \"{password}\" }}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(route, httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Login failed: {response.ReasonPhrase}");
            }

            IsLoggedIn = true;
            return true;
        }
    }
}

