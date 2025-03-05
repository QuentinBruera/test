using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status?> GetStatus(int id);
    }
}
