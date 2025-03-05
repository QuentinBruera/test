using NegosudModel.Dto;

namespace NegosudAPI.Services.Interfaces
{
    public interface IReasonService
    {
        Task<IEnumerable<ReasonDto>> GetReasons();
    }
}
