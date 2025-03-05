using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Dto;

namespace NegosudAPI.Repositories.Implementations
{
    public class ReasonRepository : IReasonRepository
    {
        private readonly NegosudContext _context;

        public ReasonRepository(NegosudContext context)
        {
            _context = context;
        }

        //Task<IEnumerable<ReasonDto>> GetReasons();
        public async Task<IEnumerable<ReasonDto>> GetReasons()
        {
            // Récupère tous les reasons
            return await _context.Reasons
                .Select(r => new ReasonDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Color = r.Color
                })
                .ToListAsync();
        }
    }
}
