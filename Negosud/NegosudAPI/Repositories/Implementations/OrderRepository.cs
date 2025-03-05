using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NegosudContext _context;

        public OrderRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrder(int id)
        {
            return await _context.Orders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
