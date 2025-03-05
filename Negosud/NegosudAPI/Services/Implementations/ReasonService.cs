using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;

namespace NegosudAPI.Services.Implementations
{
    public class ReasonService : IReasonService
    {
        private readonly IReasonRepository _reasonRepository;
        public ReasonService(IReasonRepository reasonRepository)
        {
            _reasonRepository = reasonRepository;
        }

        // Task<IEnumerable<ReasonDto>> GetReasons();
        public async Task<IEnumerable<ReasonDto>> GetReasons()
        {
            return await _reasonRepository.GetReasons();
        }
    }
}
