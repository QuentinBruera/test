using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _familyRepository;

        public FamilyService(IFamilyRepository familyRepository)
        {
            _familyRepository = familyRepository;
        }

        public async Task<IEnumerable<FamilyDto>> GetFamilies()
        {
            IEnumerable<Family> families = await _familyRepository.GetFamilies();

            return families.Select(f => new FamilyDto
            {
                Id = f.Id,
                Name = f.Name,
                Articles = f.Articles
            });
        }

        public async Task<FamilyDto?> GetFamily(int id)
        {
            Family? family = await _familyRepository.GetFamily(id);

            if (family == null) return null;

            return new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                Articles = family.Articles
            };
        }
    }
}
