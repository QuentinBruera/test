using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> GetOrderEntity(int id)
        {
            return await _orderRepository.GetOrder(id);
        }

        public async Task<OrderDto?> GetOrderDto(int id)
        {
            Order? order = await GetOrderEntity(id);

            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                Date = order.Date,
                TotalWithoutTaxes = order.TotalWithoutTaxes,
                StatusId = order.StatusId
            };
        }
    }
}
