using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Client.Services
{
    public class ApiService
    {
        // Instance singleton
        private static ApiService? _instance;

        // Verrou pour l'initialisation thread-safe
        private static readonly object _lock = new();

        protected readonly HttpClient _httpClient;
        public HttpClient HttpClient
        {
            get => _httpClient;
        }

        private static readonly CookieContainer _cookieContainer = new();

        // Constructeur privé pour empêcher l'instanciation directe
        private ApiService()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7054/")
            };
        }

        // Méthode pour obtenir l'instance unique
        public static ApiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new ApiService();
                    }
                }
                return _instance;
            }
        }

        public CookieCollection GetCookies()
        {
            return _cookieContainer.GetCookies(new Uri("https://localhost:7054/"));
        }
    }
}