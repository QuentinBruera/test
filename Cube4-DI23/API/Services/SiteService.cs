using API.Repositories;
using Model.Dto;
using Model.Entities;

namespace API.Services
{
    public class SiteService
    {
        private readonly SiteRepository _siteRepository;
        public SiteService(SiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }

        public async Task<IEnumerable<SiteDto>> GetSites()
        {
            IEnumerable<Site> sites = await _siteRepository.GetSites();
            return sites.Select(s => new SiteDto
            {
                Id = s.Id,
                City = s.City
            });
        }

        public async Task<SiteDto> GetSite(int id)
        {
            Site site = await _siteRepository.GetSite(id);
            if (site == null)
            {
                return null;
            }
            return new SiteDto
            {
                Id = site.Id,
                City = site.City
            };
        }

        public async Task<SiteDto> AddSite(SiteDto siteDto)
        {
            Site newSite = siteDto.ToEntity();
            newSite = await _siteRepository.AddSite(newSite);
            return new SiteDto
            {
                Id = newSite.Id,
                City = newSite.City
            };
        }
    }
}
