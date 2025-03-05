using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class ArticleOrderService : IArticleOrderService
    {
        private readonly IArticleOrderRepository _articleOrderRepository;

        public ArticleOrderService(IArticleOrderRepository articleOrderRepository)
        {
            _articleOrderRepository = articleOrderRepository;
        }

        public async Task<ArticleOrderDto?> GetArticleOrder(int id)
        {
            ArticleOrder? articleOrder = await _articleOrderRepository.GetArticleOrder(id);

            if (articleOrder == null) return null;

            return new ArticleOrderDto
            {
                Id = articleOrder.Id,
                Quantity = articleOrder.Quantity,
                UnitPrice = articleOrder.UnitPrice,
                TVA = articleOrder.TVA,
                ArticleId = articleOrder.ArticleId,
                OrderId = articleOrder.OrderId
            };
        }

        public async Task<List<ArticleOrder>> GetArticleOrderByOrderId(int orderId)
        {
            List<ArticleOrder> articleOrders = await _articleOrderRepository.GetArticleOrdersByOrderId(orderId);
            return articleOrders;
        }

        public async Task<List<ArticleOrderDto>> GetArticleOrderDtoByOrderId(int orderId)
        {
            List<ArticleOrder> articleOrders = await GetArticleOrderByOrderId(orderId);
            return articleOrders.Select(articleOrder => articleOrder.ToDto()).ToList();
        }

        public async Task CreateArticleOrder(ArticleOrder articleOrder)
        {
            if (articleOrder == null) throw new ArgumentNullException(nameof(articleOrder), "ArticleOrder cannot be null.");
            await _articleOrderRepository.CreateArticleOrder(articleOrder);
        }
    }
}
