using NegosudModel.Dto;

namespace NegosudAPI.Services.Interfaces
{
    public interface IFamilyService
    {
        Task<IEnumerable<FamilyDto>> GetFamilies();
        Task<FamilyDto?> GetFamily(int id);
    }
}
