using Microsoft.EntityFrameworkCore;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Context;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Repositories.Implementations
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly NegosudContext _context;

        public ArticleRepository(NegosudContext context)
        {
            _context = context;
        }

        public async Task<Article?> GetArticle(int id)
        {
            return await _context.Articles
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Article>> GetArticlesByIds(List<int> ids)
        {
            return await _context.Articles
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _context.Articles
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .ToListAsync();
        }

        public async Task<IEnumerable<ArticleDetailsDto>> GetArticlesDetails()
        {
            return await _context.Articles
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .Select(a => new ArticleDetailsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    TVA = a.TVA,
                    Description = a.Description,
                    UnitPrice = a.UnitPrice,
                    Quantity = a.Quantity,
                    MinimumQuantity = a.MinimumQuantity,
                    IsActive = a.IsActive,
                    Supplier = a.Supplier,
                    Family = a.Family
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesBySupplierId(int supplierId)
        {
            return await _context.Articles
                .Where(a => a.Supplier != null && a.Supplier.Id == supplierId)
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesForRestocking()
        {
            return await _context.Articles
                .Where(a => a.Quantity <= a.MinimumQuantity)
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .ToListAsync();
        }

        public async Task<Article?> GetArticleByName(string name)
        {
            return await _context.Articles
                .Include(a => a.Supplier)
                .Include(a => a.Family)
                .FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task CreateArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public NegosudContext GetContext()
        {
            return _context;
        }
    }
}
