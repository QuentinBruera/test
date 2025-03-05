using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;
using System.Collections.ObjectModel;

namespace Negosud.ViewModels.Inventories
{
    public class ViewInventoryViewModel : BaseViewModel
    {
        private readonly InventoryService _inventoryService;
        private readonly ArticleService _articleService;
        private readonly ArticleInventoryService _articleInventoryService;
        public InventoryDto? Inventory { get; private set; }
        public ObservableCollection<ArticleInventoryViewModel> ArticleInventories { get; private set; } = new();

        public ViewInventoryViewModel(int inventoryId)
        {
            _inventoryService = new InventoryService();
            _articleInventoryService = new ArticleInventoryService();
            _articleService = new ArticleService();

            _ = LoadInventoryAndArticlesAsync(inventoryId);
        }

        private async Task LoadInventoryAndArticlesAsync(int inventoryId)
        {
            Inventory = await _inventoryService.GetInventoryByIdAsync(inventoryId);
            OnPropertyChanged(nameof(Inventory));
            if (Inventory != null) await LoadArticleInventoriesAsync(inventoryId);
        }

        private async Task LoadArticleInventoriesAsync(int inventoryId)
        {
            IEnumerable<ArticleInventoryDto> articleInventoriesFromApi = await _articleInventoryService.GetArticleInventoriesByInventoryId(inventoryId);

            ArticleInventories.Clear();

            foreach (ArticleInventoryDto? articleInventoryDto in articleInventoriesFromApi)
            {
                ArticleDto? articleDto = await _articleService.GetArticleByIdAsync(articleInventoryDto.ArticleId);
                Article? article = articleDto?.ToEntity();

                if (article != null)
                {

                    ArticleInventory? articleInventory = new()
                    {
                        Id = articleInventoryDto.Id,
                        QuantityBefore = articleInventoryDto.QuantityBefore,
                        QuantityAfter = articleInventoryDto.QuantityAfter,
                        ArticleId = articleInventoryDto.ArticleId,
                        Article = article
                    };

                    ArticleInventories.Add(new ArticleInventoryViewModel(articleInventory));
                }
            }

            OnPropertyChanged(nameof(ArticleInventories));
        }
    }
}
