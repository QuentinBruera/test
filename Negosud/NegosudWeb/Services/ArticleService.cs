using NegosudModel.Dto;
using Newtonsoft.Json;

namespace NegosudWeb.Services
{
    public class ArticleService
    {
        private readonly HttpClient _httpClient;
        private readonly SupplierService _supplierService;

        // On injecte le HttpClient en dépendance
        public ArticleService(HttpClient httpClient, SupplierService supplierService)
        {
            _httpClient = httpClient;
            _supplierService = supplierService;
        }

        // Récupère le détail des articles
        public async Task<IEnumerable<ArticleDetailsDto>> GetArticlesDetailsAsync()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)); // Timeout de 5 sec
            var response = await _httpClient.GetAsync("api/articles/details", cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                // on peut lever une exception ou gérer autrement
                throw new Exception($"Erreur Http : {response.StatusCode}");
            }

            var resultat = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<ArticleDetailsDto>>(resultat)
                   ?? Enumerable.Empty<ArticleDetailsDto>();
        }

        public async Task<ArticleDetailsDto?> GetArticleByIdAsync(int articleId)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var response = await _httpClient.GetAsync($"api/articles/{articleId}", cts.Token);

            if (!response.IsSuccessStatusCode) return null;
            
            var json = await response.Content.ReadAsStringAsync();
            var article = JsonConvert.DeserializeObject<ArticleDetailsDto>(json);

            if (article != null && article.SupplierId > 0)
            {
                var supplierDto = await _supplierService.GetSupplierByIdAsync(article.SupplierId);
                if (supplierDto != null) article.Supplier = supplierDto.ToEntity(); 
            }

            return article;
        }
    }
}
