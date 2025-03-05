using NegosudModel.Dto;
using NegosudModel.Entities;
using System.Threading.Tasks;

namespace NegosudAPI.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDto?> GetArticleDto(int id);
        Task<Article?> GetArticleEntity(int id);
        Task<IEnumerable<ArticleDto>> GetArticles();
        Task<IEnumerable<ArticleDetailsDto>> GetArticlesDetails();
        Task<List<Article>> GetArticlesByIds(List<int> ids);
        Task<IEnumerable<ArticleDto>> GetArticlesBySupplierId(int supplierId);
        Task<IEnumerable<ArticleDto>> GetArticlesForRestocking();
        Task<ArticleDto?> GetArticleByName(string name);
        Task<bool> UpdateArticle(int id, ArticleDto articleDto);
        Task<ArticleDto> CreateArticle(ArticleDto articleDto);
        Task UpdateArticle(Article article);
    }
}
