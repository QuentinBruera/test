using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Implementations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IArticleOrderService _articleOrderService;
        private readonly IArticleService _articleService;
        private readonly ISupplierService _supplierService;
        private readonly IStatusService _statusService;

        public PurchaseService(IPurchaseRepository purchaseRepository, IArticleOrderService articleOrderService, IArticleService articleService, ISupplierService supplierService, IStatusService statusService)
        {
            _purchaseRepository = purchaseRepository;
            _articleOrderService = articleOrderService;
            _articleService = articleService;
            _supplierService = supplierService;
            _statusService = statusService;
        }

        public async Task<PurchaseDto?> GetPurchaseDto(int id)
        {
            Purchase? purchase = await _purchaseRepository.GetPurchase(id);
            if (purchase == null) return null;
            
            List<ArticleOrder> articleOrders = await _articleOrderService.GetArticleOrderByOrderId(purchase.Id);
            double sumOfArticleOrdersTotalWithTaxes = Math.Round(articleOrders.Sum(a => a.GetTotalWithTaxes()),2);

            return purchase.ToDto(sumOfArticleOrdersTotalWithTaxes);
        }

        public async Task<IEnumerable<PurchaseDto>> GetPurchases()
        {
            IEnumerable<Purchase> purchases = await _purchaseRepository.GetPurchases();

            return purchases.Select(p => new PurchaseDto
            {
                Id = p.Id,
                Date = p.Date,
                TotalWithoutTaxes = p.TotalWithoutTaxes,
                StatusId = p.Status != null ? p.Status.Id : 0,
                SupplierId = p.Supplier != null ? p.Supplier.Id : 0
            });
        }

        public async Task<IEnumerable<PurchaseDto>> GetReceptions()
        {
            IEnumerable<Purchase> receptions = await _purchaseRepository.GetReceptions();

            return receptions.Select(p => new PurchaseDto
            {
                Id = p.Id,
                Date = p.Date,
                TotalWithoutTaxes = p.TotalWithoutTaxes,
                StatusId = p.Status != null ? p.Status.Id : 0,
                SupplierId = p.Supplier != null ? p.Supplier.Id : 0
            });
        }

        public async Task<int> CreatePurchaseWithArticles(CreatePurchaseRequest request)
        {
            Purchase createdPurchase = await CreatePurchase(request);

            List<int> articleIds = request.ArticleQuantities.Select(a => a.Article!.Id).ToList();
            List<Article> articles = await _articleService.GetArticlesByIds(articleIds);

            if (articles.Count != articleIds.Count) throw new ArgumentException("One or more articles do not exist.");

            foreach (ArticleQuantity articleQte in request.ArticleQuantities)
            {
                if (articleQte.Article == null) throw new ArgumentException("Article in ArticleQuantities cannot be null.");

                Article? article = articles.First(a => a.Id == articleQte.Article.Id);

                ArticleOrder newArticleOrder = new ArticleOrder
                {
                    Quantity = articleQte.Quantity,
                    UnitPrice = article.UnitPrice,
                    TVA = article.TVA,
                    Article = article,
                    Order = createdPurchase
                };

                await _articleOrderService.CreateArticleOrder(newArticleOrder);
            }

            return createdPurchase.Id;
        }

        public async Task<Purchase> CreatePurchase(PurchaseDto purchaseDto)
        {
            if (purchaseDto.Date == default) throw new ArgumentException("The date provided is invalid.");

            if (purchaseDto.SupplierId <= 0) throw new ArgumentException("SupplierId cannot be null or 0 when creating a purchase.");
            Supplier? supplier = await _supplierService.GetSupplierEntity(purchaseDto.SupplierId) ?? throw new ArgumentException($"Supplier with ID {purchaseDto.SupplierId} does not exist.");

            Status? status = await _statusService.GetStatusEntity(8) ?? throw new ArgumentException("Default status 'Pending' with ID 8 does not exist.");

            Purchase? purchase = new Purchase
            {
                Date = purchaseDto.Date,
                TotalWithoutTaxes = purchaseDto.TotalWithoutTaxes,
                Status = status,
                Supplier = supplier
            };

            await _purchaseRepository.CreatePurchase(purchase);
            return purchase;
        }

        public async Task<Purchase> CreatePurchase(CreatePurchaseRequest request)
        {
            if (request.Date == default) throw new ArgumentException("The date provided is invalid.");

            PurchaseDto purchaseDto = new PurchaseDto
            {
                SupplierId = request.SupplierId,
                Date = request.Date,
                TotalWithoutTaxes = Math.Round(request.ArticleQuantities.Sum(articleQte => articleQte.Article!.UnitPrice * articleQte.Quantity), 2)
            };

            return await CreatePurchase(purchaseDto);
        }

        public async Task<int> CreatePurchaseRestocking(int articleId)
        {
            Article? article = await _articleService.GetArticleEntity(articleId);
            if (article == null) throw new ArgumentException($"Article with ID {articleId} does not exist.");
            if (article.Quantity > article.MinimumQuantity) throw new InvalidOperationException($"Article '{article.Name}' stock is above the minimum threshold.");
            if (article.SupplierId <= 0) throw new InvalidOperationException($"Article '{article.Name}' does not have an associated supplier.");

            Supplier? supplier = await _supplierService.GetSupplierEntity(article.SupplierId);
            if (supplier == null) throw new ArgumentException($"Supplier with ID {article.SupplierId} does not exist.");

            CreatePurchaseRequest request = new CreatePurchaseRequest
            {
                SupplierId = supplier.Id,
                Date = DateTime.UtcNow,
                ArticleQuantities = new List<ArticleQuantity>
                {
                    new ArticleQuantity
                    {
                        Article = article.ToDto(),
                        Quantity = article.MinimumQuantity
                    }
                }
            };

            return await CreatePurchaseWithArticles(request);
        }

        public async Task<bool> CancelPurchase(int id)
        {
            Purchase? purchase = await _purchaseRepository.GetPurchase(id);
            if (purchase == null) return false;

            Status? status = await _statusService.GetStatusEntity(10);
            if (status == null) throw new ArgumentException("Status does not exist.");

            purchase.StatusId = status.Id;
            await _purchaseRepository.UpdatePurchase(purchase);

            return true;
        }

        public async Task<bool> ReceivePurchase(int id)
        {
            Purchase? purchase = await _purchaseRepository.GetPurchase(id);
            if (purchase == null) return false;

            Status? status = await _statusService.GetStatusEntity(9);
            if (status == null) throw new ArgumentException("Status does not exist.");

            List<ArticleOrder> articleOrders = await _articleOrderService.GetArticleOrderByOrderId(purchase.Id);
            foreach (var articleOrder in articleOrders)
            {
                if (articleOrder.Article == null)
                    throw new InvalidOperationException($"Article with ID {articleOrder.ArticleId} not found.");

                articleOrder.Article.Quantity += articleOrder.Quantity;

                await _articleService.UpdateArticle(articleOrder.Article);
            }

            purchase.StatusId = status.Id;
            await _purchaseRepository.UpdatePurchase(purchase);

            return true;
        }

        public async Task<bool> DeletePurchase(int purchaseId)
        {
            Purchase? purchase = await _purchaseRepository.GetPurchase(purchaseId);
            if (purchase == null) return false;

            await _purchaseRepository.DeletePurchaseWithArticles(purchaseId);
            return true;
        }
    }
}
