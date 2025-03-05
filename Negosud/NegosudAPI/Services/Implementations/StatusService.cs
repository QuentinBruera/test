using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<Status?> GetStatusEntity(int id)
        {
            return await _statusRepository.GetStatus(id);
        }

        public async Task<StatusDto?> GetStatus(int id)
        {
            Status? status = await GetStatusEntity(id);

            if (status == null) return null;

            return new StatusDto
            {
                Id = status.Id,
                Name = status.Name,
                Color = status.Color
            };
        }
    }
}
