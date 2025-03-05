using NegosudModel.Context;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        Task<Article?> GetArticle(int id);
        Task<IEnumerable<Article>> GetArticles();
        Task<IEnumerable<ArticleDetailsDto>> GetArticlesDetails();
        Task<List<Article>> GetArticlesByIds(List<int> ids);
        Task<IEnumerable<Article>> GetArticlesBySupplierId(int supplierId);
        Task<IEnumerable<Article>> GetArticlesForRestocking();
        Task<Article?> GetArticleByName(string name);
        Task UpdateArticle(Article article);
        Task CreateArticle(Article article);
        NegosudContext GetContext();
    }
}
