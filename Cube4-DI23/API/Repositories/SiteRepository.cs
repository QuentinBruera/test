using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace API.Repositories
{
    public class SiteRepository
    {
        private readonly DirectoryDbContext _context;

        public SiteRepository(DirectoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Site>> GetSites()
        {
            return await _context.Site.ToListAsync();
        }

        public async Task<Site> GetSite(int id)
        {
            return await _context.Site.FindAsync(id);
        }

        public async Task<Site> AddSite(Site site)
        {
            _context.Site.Add(site);
            await _context.SaveChangesAsync();
            return site;
        }
    }
}
