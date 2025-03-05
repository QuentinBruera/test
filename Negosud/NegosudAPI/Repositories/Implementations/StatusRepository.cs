using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class StatusRepository : IStatusRepository
    {
        private readonly NegosudContext _context;

        public StatusRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Status?> GetStatus(int id)
        {
            return await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
