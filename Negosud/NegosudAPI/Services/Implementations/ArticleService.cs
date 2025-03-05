using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ISupplierService _supplierService;
        private readonly IFamilyService _familyService;

        public ArticleService(IArticleRepository articleRepository, ISupplierService supplierService, IFamilyService familyService)
        {
            _articleRepository = articleRepository;
            _supplierService = supplierService;
            _familyService = familyService;
        }

        public async Task<ArticleDto?> GetArticleDto(int id)
        {
            Article? article = await _articleRepository.GetArticle(id);

            if (article == null) return null;

            return new ArticleDto
            {
                Id = article.Id,
                Name = article.Name,
                TVA = article.TVA,
                Description = article.Description,
                UnitPrice = article.UnitPrice,
                Quantity = article.Quantity,
                MinimumQuantity = article.MinimumQuantity,
                IsActive = article.IsActive,
                SupplierId = article.SupplierId,
                FamilyId = article.FamilyId
            };
        }

        public async Task<Article?> GetArticleEntity(int id)
        {
            return await _articleRepository.GetArticle(id);
        }

        public async Task<List<Article>> GetArticlesByIds(List<int> ids)
        {
            if (ids == null || ids.Count == 0) throw new ArgumentException("The list of IDs cannot be null or empty.");
            return await _articleRepository.GetArticlesByIds(ids);
        }

        public async Task<IEnumerable<ArticleDto>> GetArticles()
        {
            IEnumerable<Article> articles = await _articleRepository.GetArticles();

            return articles.Select(a => new ArticleDto
            {
                Id = a.Id,
                Name = a.Name,
                TVA = a.TVA,
                Description = a.Description,
                UnitPrice = a.UnitPrice,
                Quantity = a.Quantity,
                MinimumQuantity = a.MinimumQuantity,
                IsActive = a.IsActive,
                SupplierId = a.Supplier != null ? a.Supplier.Id : 0,
                FamilyId = a.Family != null ? a.Family.Id : 0
            });
        }

        public async Task<IEnumerable<ArticleDetailsDto>> GetArticlesDetails()
        {
            IEnumerable<ArticleDetailsDto> articles = await _articleRepository.GetArticlesDetails();

            return articles.Select(a => new ArticleDetailsDto
            {
                Id = a.Id,
                Name = a.Name,
                TVA = a.TVA,
                Description = a.Description,
                UnitPrice = a.UnitPrice,
                Quantity = a.Quantity,
                MinimumQuantity = a.MinimumQuantity,
                IsActive = a.IsActive,
                SupplierId = a.Supplier != null ? a.Supplier.Id : 0,
                Supplier = a.Supplier,
                FamilyId = a.Family != null ? a.Family.Id : 0,
                Family = a.Family
            });
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesBySupplierId(int supplierId)
        {
            IEnumerable<Article> articles = await _articleRepository.GetArticlesBySupplierId(supplierId);

            return articles.Select(a => new ArticleDto
            {
                Id = a.Id,
                Name = a.Name,
                TVA = a.TVA,
                Description = a.Description,
                UnitPrice = a.UnitPrice,
                Quantity = a.Quantity,
                MinimumQuantity = a.MinimumQuantity,
                IsActive = a.IsActive,
                SupplierId = a.Supplier != null ? a.Supplier.Id : 0,
                FamilyId = a.Family != null ? a.Family.Id : 0
            });
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesForRestocking()
        {
            IEnumerable<Article> articles = await _articleRepository.GetArticlesForRestocking();

            return articles.Select(a => new ArticleDto
            {
                Id = a.Id,
                Name = a.Name,
                TVA = a.TVA,
                Description = a.Description,
                UnitPrice = a.UnitPrice,
                Quantity = a.Quantity,
                MinimumQuantity = a.MinimumQuantity,
                IsActive = a.IsActive,
                SupplierId = a.Supplier != null ? a.Supplier.Id : 0,
                FamilyId = a.Family != null ? a.Family.Id : 0
            });
        }

        public async Task<ArticleDto?> GetArticleByName(string name)
        {
            Article? article = await _articleRepository.GetArticleByName(name);

            if (article == null) return null;
         
            return new ArticleDto
            {
                Id = article.Id,
                Name = article.Name,
                TVA = article.TVA,
                Description = article.Description,
                UnitPrice = article.UnitPrice,
                Quantity = article.Quantity,
                MinimumQuantity = article.MinimumQuantity,
                IsActive = article.IsActive,
                SupplierId = article.Supplier != null ? article.Supplier.Id : 0,
                FamilyId = article.Family != null ? article.Family.Id : 0
            };
        }

        public async Task<bool> UpdateArticle(int id, ArticleDto articleDto)
        {
            Article? existingArticle = await _articleRepository.GetArticle(id);
            if (existingArticle == null) return false;

            if (articleDto.SupplierId > 0)
            {
                SupplierDto? supplierDto = await _supplierService.GetSupplierDto(articleDto.SupplierId);
                if (supplierDto == null) throw new ArgumentException($"Supplier with ID {articleDto.SupplierId} does not exist.");
                existingArticle.Supplier = _articleRepository.GetContext().Suppliers.Local.FirstOrDefault(s => s.Id == supplierDto.Id)
                                            ?? supplierDto.ToEntity();
            }
            else
            {
                existingArticle.Supplier = null;
            }

            if (articleDto.FamilyId > 0)
            {
                FamilyDto? family = await _familyService.GetFamily(articleDto.FamilyId);
                if (family == null) throw new ArgumentException($"Family with ID {articleDto.FamilyId} does not exist.");
                existingArticle.Family = _articleRepository.GetContext().Families.Local.FirstOrDefault(f => f.Id == family.Id)
                                         ?? family.ToEntity();
            }
            else
            {
                existingArticle.Family = null;
            }

            existingArticle.Name = articleDto.Name;
            existingArticle.TVA = articleDto.TVA;
            existingArticle.Description = articleDto.Description;
            existingArticle.UnitPrice = articleDto.UnitPrice;
            existingArticle.Quantity = articleDto.Quantity;
            existingArticle.MinimumQuantity = articleDto.MinimumQuantity;
            existingArticle.IsActive = articleDto.IsActive;

            await _articleRepository.UpdateArticle(existingArticle);
            return true;
        }

        public async Task<ArticleDto> CreateArticle(ArticleDto articleDto)
        {
            Supplier? supplier = null;
            if (articleDto.SupplierId > 0)
            {
                SupplierDto? supplierDto = await _supplierService.GetSupplierDto(articleDto.SupplierId);
                if (supplierDto == null) throw new ArgumentException($"Supplier with ID {articleDto.SupplierId} does not exist.");
                supplier = supplierDto.ToEntity();
            }

            Family? family = null;
            if (articleDto.FamilyId > 0)
            {
                FamilyDto? familyDto = await _familyService.GetFamily(articleDto.FamilyId);
                if (familyDto == null) throw new ArgumentException($"Family with ID {articleDto.FamilyId} does not exist.");
                family = familyDto.ToEntity();
            }

            Article? article = new Article
            {
                Name = articleDto.Name,
                TVA = articleDto.TVA,
                Description = articleDto.Description,
                UnitPrice = articleDto.UnitPrice,
                Quantity = articleDto.Quantity,
                MinimumQuantity = articleDto.MinimumQuantity,
                IsActive = articleDto.IsActive,
                Supplier = supplier,
                Family = family
            };

            await _articleRepository.CreateArticle(article);
            articleDto.Id = article.Id;
            return articleDto;
        }

        public async Task UpdateArticle(Article article)
        {
            await _articleRepository.UpdateArticle(article);
        }
    }
}
