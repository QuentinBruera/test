using System.Net;
using System.Net.Http;

namespace Negosud.Services
{
    public class ApiService
    {
        protected readonly HttpClient _httpClient;
        private static readonly CookieContainer _cookieContainer = new();

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer
            };

            //_httpClient = new HttpClient(handler)
            //{
            //    BaseAddress = new Uri("https://localhost:7024/")
            //};

            // Cette syntaxe sert à effectuer la conditon au moment de la compilation
            #if DEBUG
            _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7024/")
                };
            #else
                _httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri("http://localhost:5000/")
                };
            #endif
        }

        protected static CookieCollection GetCookies()
        {
            //return _cookieContainer.GetCookies(new Uri("https://localhost:7024/"));

            #if DEBUG
                return _cookieContainer.GetCookies(new Uri("https://localhost:7024/"));
            #else
                return _cookieContainer.GetCookies(new Uri("http://localhost:5000/"));
            #endif
        }
    }
}