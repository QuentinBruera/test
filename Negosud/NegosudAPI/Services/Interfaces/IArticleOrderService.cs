using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Interfaces
{
    public interface IArticleOrderService
    {
        Task<ArticleOrderDto?> GetArticleOrder(int id);
        Task<List<ArticleOrder>> GetArticleOrderByOrderId(int orderId);
        Task<List<ArticleOrderDto>> GetArticleOrderDtoByOrderId(int orderId);
        Task CreateArticleOrder(ArticleOrder articleOrder);
    }
}
