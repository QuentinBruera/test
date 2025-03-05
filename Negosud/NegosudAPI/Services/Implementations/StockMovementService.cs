using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Services.Implementations
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IArticleService _articleService;
        private readonly IReasonService _reasonService;
        public StockMovementService(IStockMovementRepository stockMovementRepository, IArticleService articleService)
        {
            _stockMovementRepository = stockMovementRepository;
            _articleService = articleService;
        }

        public async Task<List<StockMovementDto>> GetStockMovementsByArticleId(int articleId)
        {
            List<StockMovement> stockMovements = await _stockMovementRepository.GetStockMovementsByArticleId(articleId);
            return stockMovements.Select(sm => sm.ToDto()).ToList();
        }

        public async Task<StockMovementDto> CreateStockMovement(StockMovementDto stockMovementDto)
        {
            Article? article = null;
            if (stockMovementDto.ArticleId > 0)
            {
                article = await _articleService.GetArticleEntity(stockMovementDto.ArticleId);
                if (article == null) throw new ArgumentException("The article does not exist.");
            }

            Reason? reason = null;
            if (stockMovementDto.ReasonId > 0)
            {
                reason = stockMovementDto.Reason.ToEntity();
            }
            // Générer un ID basé sur un timestamp
            int timestamp = (int)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % int.MaxValue);

            StockMovement stockMovement = new StockMovement
            {
                Id = timestamp,
                Date = stockMovementDto.Date,
                Quantity = stockMovementDto.Quantity,
                ArticleId = stockMovementDto.ArticleId,
                Article = article,
                ReasonId = stockMovementDto.ReasonId,
                Reasons = reason,
            };

            await _stockMovementRepository.CreateStockMovement(stockMovement);
            stockMovementDto.Id = stockMovement.Id;
            return stockMovementDto;
        }
    }
}
