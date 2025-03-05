using System.Net.Http;
using System.Net.Http.Json;
using NegosudModel.Dto;

namespace Negosud.Services
{
    public class ArticleService : ApiService
    {
        public async Task<IEnumerable<ArticleDto>> GetArticlesBySupplierIdAsync(int supplierId)
        {
            try
            {
                IEnumerable<ArticleDto>? articles = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDto>>($"api/articles?supplierId={supplierId}");
                return articles ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving articles : {ex.Message}");
                return [];
            }
        }

        public async Task<ArticleDto?> GetArticleByIdAsync(int articleId)
        {
            try
            {
                ArticleDto? article = await _httpClient.GetFromJsonAsync<ArticleDto>($"api/articles/{articleId}");
                return article;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving article with ID {articleId} : {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync()
        {
            try
            {
                IEnumerable<ArticleDto>? articles = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDto>>("api/articles");
                return articles ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving articles : {ex.Message}");
                return [];
            }
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesForRestockingAsync()
        {
            try
            {
                IEnumerable<ArticleDto>? articles = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDto>>("api/articles/restocking");
                return articles ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving articles for restocking: {ex.Message}");
                return [];
            }
        }

        public async Task<IEnumerable<ArticleDetailsDto>> GetArticlesDetailsAsync()
        {
            try
            {
                IEnumerable<ArticleDetailsDto>? articles = await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDetailsDto>>("api/articles/details");
                return articles ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des articles : {ex.Message}");
                return [];
            }
        }

        public async Task<bool> UpdateArticleAsync(ArticleDto request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/articles/{request.Id}", request);
                if (response.IsSuccessStatusCode)
                {
                    ArticleDto? updatedArticle = await response.Content.ReadFromJsonAsync<ArticleDto>();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating article : {ex.Message}");
                return false;
            }
        }
    }
}

