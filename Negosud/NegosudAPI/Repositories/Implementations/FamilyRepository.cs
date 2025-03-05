using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly NegosudContext _context;

        public FamilyRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Family>> GetFamilies()
        {
            return await _context.Families.ToListAsync();
        }

        public async Task<Family?> GetFamily(int id)
        {
            return await _context.Families.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
