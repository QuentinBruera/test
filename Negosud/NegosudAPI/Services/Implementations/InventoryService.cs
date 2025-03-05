using NegosudAPI.Services.Interfaces;
using NegosudAPI.Repositories.Interfaces;
using NegosudModel.Entities;
using NegosudModel.Request;
using NegosudAPI.Repositories.Implementations;
using NegosudModel.Dto;

namespace NegosudAPI.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IArticleService _articleService;
        private readonly IArticleInventoryService _articleInventoryService;

        public InventoryService(IInventoryRepository inventoryRepository, IArticleService articleService, IArticleInventoryService articleInventoryService) 
        {
            _inventoryRepository = inventoryRepository;
            _articleService = articleService;
            _articleInventoryService = articleInventoryService;
        }

        public async Task<InventoryDto?> GetInventoryDto(int id)
        {
            Inventory? inventory = await _inventoryRepository.GetInventory(id);
            if (inventory == null) return null;

            return inventory.ToDto();
        }

        public async Task<IEnumerable<InventoryDto>> GetInventories()
        {
            IEnumerable<Inventory> inventories = await _inventoryRepository.GetInventories();

            return inventories.Select(i => new InventoryDto
            {
                Id = i.Id,
                Date = i.Date
            });
        }

        public async Task<int> CreateInventoryWithArticles(CreateInventoryRequest request)
        {
            Inventory createdInventory = await CreateInventory(request);

            List<int> articleIds = request.ArticleInventories.Select(a => a.ArticleId).ToList();
            List<Article> articles = await _articleService.GetArticlesByIds(articleIds);

            if (articles.Count != articleIds.Count) throw new ArgumentException("One or more articles do not exist.");

            foreach (ArticleInventoryRequest articleInventoryRequest in request.ArticleInventories)
            {
                Article? article = articles.FirstOrDefault(a => a.Id == articleInventoryRequest.ArticleId);
                if (article == null) throw new ArgumentException($"Article with ID {articleInventoryRequest.ArticleId} does not exist.");

                ArticleInventory newArticleInventory = new ArticleInventory
                {
                    ArticleId = articleInventoryRequest.ArticleId,
                    QuantityBefore = articleInventoryRequest.QuantityBefore,
                    QuantityAfter = articleInventoryRequest.QuantityAfter,
                    Inventory = createdInventory,
                    Article = article
                };
                await _articleInventoryService.CreateArticleInventory(newArticleInventory);

                article.Quantity = articleInventoryRequest.QuantityAfter;
                await _articleService.UpdateArticle(article.Id, new ArticleDto
                {
                    Id = article.Id,
                    Name = article.Name,
                    TVA = article.TVA,
                    Description = article.Description,
                    UnitPrice = article.UnitPrice,
                    Quantity = articleInventoryRequest.QuantityAfter,
                    MinimumQuantity = article.MinimumQuantity,
                    IsActive = article.IsActive,
                    SupplierId = article.SupplierId,
                    FamilyId = article.FamilyId
                });
            }

            return createdInventory.Id;
        }

        public async Task<Inventory> CreateInventory(InventoryDto inventoryDto)
        {
            if (inventoryDto.Date == default) throw new ArgumentException("The date provided is invalid.");

            Inventory? inventory = new Inventory
            {
                Date = inventoryDto.Date
            };

            await _inventoryRepository.CreateInventory(inventory);
            return inventory;
        }

        public async Task<Inventory> CreateInventory(CreateInventoryRequest request)
        {
            if (request.Date == default) throw new ArgumentException("The date provided is invalid.");

            InventoryDto inventoryDto = new InventoryDto
            {
                Date = request.Date
            };
            return await CreateInventory(inventoryDto);
        }

        public async Task<bool> DeleteInventory(int inventoryId)
        {
            Inventory? inventory = await _inventoryRepository.GetInventory(inventoryId);
            if (inventory == null) return false;

            await _inventoryRepository.DeleteInventoryWithArticles(inventoryId);
            return true;
        }
    }
}
