using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace NegosudAPI.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IArticleOrderService _articleOrderService;
        private readonly IArticleService _articleService;
        private readonly ICustomerService _customerService;
        private readonly IStatusService _statusService;

        public SaleService(ISaleRepository saleRepository, IArticleOrderService articleOrderService, IArticleService articleService, ICustomerService customerService, IStatusService statusService)
        {
            _saleRepository = saleRepository;
            _articleOrderService = articleOrderService;
            _articleService = articleService;
            _customerService = customerService;
            _statusService = statusService;
        }

        public async Task<SaleDto?> GetSaleDto(int id)
        {
            Sale? sale = await _saleRepository.GetSale(id);
            if (sale == null) return null;

            List<ArticleOrder> articleOrders = await _articleOrderService.GetArticleOrderByOrderId(sale.Id);
            double sumOfArticleOrdersTotalWithTaxes = Math.Round(articleOrders.Sum(a => a.GetTotalWithTaxes()), 2);

            return sale.ToDto(sumOfArticleOrdersTotalWithTaxes);
        }

        public async Task<IEnumerable<SaleDto>> GetSales()
        {
            IEnumerable<Sale> sales = await _saleRepository.GetSales();

            return sales.Select(p => new SaleDto
            {
                Id = p.Id,
                Date = p.Date,
                TotalWithoutTaxes = p.TotalWithoutTaxes,
                StatusId = p.Status != null ? p.Status.Id : 0,
                CustomerId = p.Customer != null ? p.Customer.Id : 0
            });
        }

        public async Task<int> CreateSaleWithArticles(CreateSaleRequest request)
        {
            Sale createdSale = await CreateSale(request);

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
                    Order = createdSale
                };

                await _articleOrderService.CreateArticleOrder(newArticleOrder);
            }

            return createdSale.Id;
        }

        public async Task<Sale> CreateSale(CreateSaleRequest request)
        {
            if (request.Date == default) throw new ArgumentException("The date provided is invalid.");

            SaleDto saleDto = new SaleDto
            {
                CustomerId = request.CustomerId,
                Date = request.Date,
                TotalWithoutTaxes = Math.Round(request.ArticleQuantities.Sum(articleQte => articleQte.Article!.UnitPrice * articleQte.Quantity), 2)
            };

            return await CreateSale(saleDto);
        }

        public async Task<Sale> CreateSale(SaleDto saleDto)
        {
            if (saleDto.Date == default) throw new ArgumentException("The date provided is invalid.");

            if (saleDto.CustomerId <= 0) throw new ArgumentException("CustomerId cannot be null or 0 when creating a sale.");
            Customer? customer = await _customerService.GetCustomerEntity(saleDto.CustomerId) ?? throw new ArgumentException($"Customer with ID {saleDto.CustomerId} does not exist.");

            Status? status = await _statusService.GetStatusEntity(8) ?? throw new ArgumentException("Default status 'Pending' with ID 8 does not exist.");

            Sale? sale = new Sale
            {
                Date = saleDto.Date,
                TotalWithoutTaxes = saleDto.TotalWithoutTaxes,
                Status = status,
                Customer = customer
            };

            await _saleRepository.CreateSale(sale);
            return sale;
        }

        public async Task<bool> CancelSale(int id)
        {
            Sale? sale = await _saleRepository.GetSale(id);
            if (sale == null) return false;

            Status? status = await _statusService.GetStatusEntity(10);
            if (status == null) throw new ArgumentException("Status does not exist.");

            sale.StatusId = status.Id;
            await _saleRepository.UpdateSale(sale);

            return true;
        }

        public async Task<bool> ReceiveSale(int id)
        {
            Sale? sale = await _saleRepository.GetSale(id);
            if (sale == null) return false;

            Status? status = await _statusService.GetStatusEntity(9);
            if (status == null) throw new ArgumentException("Status does not exist.");

            List<ArticleOrder> articleOrders = await _articleOrderService.GetArticleOrderByOrderId(sale.Id);
            foreach (var articleOrder in articleOrders)
            {
                if (articleOrder.Article == null)
                    throw new InvalidOperationException($"Article with ID {articleOrder.ArticleId} not found.");

                articleOrder.Article.Quantity += articleOrder.Quantity;

                await _articleService.UpdateArticle(articleOrder.Article);
            }

            sale.StatusId = status.Id;
            await _saleRepository.UpdateSale(sale);

            return true;
        }
        public async Task<bool> DeleteSale(int id)
        {
            Sale? sale = await _saleRepository.GetSale(id);
            if (sale == null) return false;

            await _saleRepository.DeleteSaleWithArticles(id);
            return true;
        }

    }
}
