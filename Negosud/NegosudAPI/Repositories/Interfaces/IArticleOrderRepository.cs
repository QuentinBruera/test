using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IArticleOrderRepository
    {
        NegosudContext GetContext();
        Task<ArticleOrder?> GetArticleOrder(int id);
        Task<List<ArticleOrder>> GetArticleOrdersByOrderId(int orderId);
        Task CreateArticleOrder(ArticleOrder articleOrder);
    }
}
