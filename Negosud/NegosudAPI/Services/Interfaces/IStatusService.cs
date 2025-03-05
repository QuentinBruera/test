using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Interfaces
{
    public interface IStatusService
    {
        Task<StatusDto?> GetStatus(int id);
        Task<Status?> GetStatusEntity(int id);

    }
}
