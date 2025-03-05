using NegosudModel.Dto;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IReasonRepository
    {
        Task<IEnumerable<ReasonDto>> GetReasons();
    }
}
