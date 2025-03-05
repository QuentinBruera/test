using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace Negosud.ViewModels.Purchases
{
    public class ViewPurchaseViewModel : BaseViewModel
    {
        private readonly PurchaseService _purchaseService;
        private readonly ArticleService _articleService;
        private readonly ArticleOrderService _articleOrderService;
        private readonly SupplierService _supplierService;
        public bool IsReceiveButtonVisible => Purchase?.StatusId == 8;

        private IAsyncRelayCommand? _receivePurchaseCommand;

        public ViewPurchaseViewModel(int purchaseId)
        {
            _purchaseService = new PurchaseService();
            _articleOrderService = new ArticleOrderService();
            _articleService = new ArticleService();
            _supplierService = new SupplierService();

            _ = LoadPurchaseAndArticlesAsync(purchaseId);
        }

        public PurchaseDto? Purchase { get; private set; }
        public string SupplierName { get; private set; } = string.Empty;
        public ObservableCollection<ArticleOrderViewModel> ArticleOrders { get; private set; } = new();

        public double TotalWithoutTaxes => ArticleOrders.Sum(a => a.TotalWithoutTaxes);
        public double TotalTVA => ArticleOrders.Sum(a => a.TotalWithTaxes - a.TotalWithoutTaxes);
        public double TotalWithTaxes => ArticleOrders.Sum(a => a.TotalWithTaxes);

        private async Task LoadPurchaseAndArticlesAsync(int purchaseId)
        {
            Purchase = await _purchaseService.GetPurchaseByIdAsync(purchaseId);

            if (Purchase != null)
            {
                SupplierDto? supplierDto = await _supplierService.GetSupplierByIdAsync(Purchase.SupplierId);
                SupplierName = supplierDto?.Name ?? string.Empty;

                OnPropertyChanged(nameof(IsReceiveButtonVisible));
                OnPropertyChanged(nameof(Purchase));
                OnPropertyChanged(nameof(SupplierName));

                await LoadArticleOrdersAsync(purchaseId);
            }
        }

        private async Task LoadArticleOrdersAsync(int purchaseId)
        {
            IEnumerable<ArticleOrderDto> articleOrdersFromApi = await _articleOrderService.GetArticleOrdersByPurchaseId(purchaseId);

            ArticleOrders.Clear();

            foreach (ArticleOrderDto? articleOrderDto in articleOrdersFromApi)
            {
                ArticleDto? articleDto = await _articleService.GetArticleByIdAsync(articleOrderDto.ArticleId);
                Article? article = articleDto?.ToEntity();

                if (article != null)
                {

                    ArticleOrder? articleOrder = new()
                    {
                        Id = articleOrderDto.Id,
                        Quantity = articleOrderDto.Quantity,
                        UnitPrice = articleOrderDto.UnitPrice,
                        TVA = articleOrderDto.TVA,
                        ArticleId = articleOrderDto.ArticleId,
                        Article = article
                    };

                    ArticleOrders.Add(new ArticleOrderViewModel(articleOrder));
                }
            }

            OnPropertyChanged(nameof(ArticleOrders));
            OnPropertyChanged(nameof(TotalWithoutTaxes));
            OnPropertyChanged(nameof(TotalTVA));
            OnPropertyChanged(nameof(TotalWithTaxes));
        }

        public IAsyncRelayCommand ReceivePurchaseCommand => _receivePurchaseCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                if (Purchase != null)
                {
                    bool success = await _purchaseService.ReceivePurchase(Purchase.Id);
                    if (success)
                    {
                        if (Application.Current.MainWindow?.DataContext is MainViewModel mainViewModel) mainViewModel.NavigateToPurchasesCommand.Execute(null);

                    }
                    else MessageBox.Show($"Impossible de réceptionner la commande {Purchase.Id}.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Les informations de la commande sont manquantes.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
    }
}
