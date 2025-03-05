using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Interfaces
{
    public interface IFamilyRepository
    {
        Task<IEnumerable<Family>> GetFamilies();
        Task<Family?> GetFamily(int id);
    }
}
