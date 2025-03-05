using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using Negosud.ViewModels.Purchases;
using NegosudModel.Dto;
using NegosudModel.Entities;
using System.Collections.ObjectModel;
using System.Windows;

namespace Negosud.ViewModels.Sales
{
    public class ViewSaleViewModel : BaseViewModel
    {
        private readonly SaleService _saleService;
        private readonly ArticleService _articleService;
        private readonly ArticleOrderService _articleOrderService;
        private readonly CustomerService _customerService;
        public bool IsReceiveButtonVisible => Sale?.StatusId == 8;

        private IAsyncRelayCommand? _receiveSaleCommand;

        public ViewSaleViewModel(int saleId) 
        {
            _saleService = new SaleService();
            _articleOrderService = new ArticleOrderService();
            _articleService = new ArticleService();
            _customerService = new CustomerService();

            _ = LoadSaleAndArticlesAsync(saleId);
        }

        public SaleDto? Sale { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public ObservableCollection<ArticleOrderViewModel> ArticleOrders { get; private set; } = new();

        public double TotalWithoutTaxes => ArticleOrders.Sum(a => a.TotalWithoutTaxes);
        public double TotalTVA => ArticleOrders.Sum(a => a.TotalWithTaxes - a.TotalWithoutTaxes);
        public double TotalWithTaxes => ArticleOrders.Sum(a => a.TotalWithTaxes);

        private async Task LoadSaleAndArticlesAsync(int saleId)
        {
            Sale = await _saleService.GetSaleByIdAsync(saleId);

            if (Sale != null)
            {
                CustomerDto? customerDto = await _customerService.GetCustomerByIdAsync(Sale.CustomerId);
                CustomerName = customerDto?.FullName ?? string.Empty;

                OnPropertyChanged(nameof(IsReceiveButtonVisible));
                OnPropertyChanged(nameof(Sale));
                OnPropertyChanged(nameof(CustomerName));

                await LoadArticleOrdersAsync(saleId);
            }
        }

        private async Task LoadArticleOrdersAsync(int saleId)
        {
            IEnumerable<ArticleOrderDto> articleOrdersFromApi = await _articleOrderService.GetArticleOrdersByPurchaseId(saleId);

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

        public IAsyncRelayCommand ReceiveSaleCommand => _receiveSaleCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                if (Sale != null)
                {
                    bool success = await _saleService.ReceiveSale(Sale.Id);
                    if (success)
                    {
                        if (Application.Current.MainWindow?.DataContext is MainViewModel mainViewModel) mainViewModel.NavigateToSalesCommand.Execute(null);

                    }
                    else MessageBox.Show($"Impossible de réceptionner la vente {Sale.Id}.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Les informations de la vente sont manquantes.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
    }
}
