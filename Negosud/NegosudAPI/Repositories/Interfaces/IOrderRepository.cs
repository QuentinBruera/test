using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrder(int id);
    }
}
