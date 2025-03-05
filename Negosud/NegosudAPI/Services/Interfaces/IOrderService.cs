using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto?> GetOrderDto(int id);
        Task<Order?> GetOrderEntity(int id);
    }
}
