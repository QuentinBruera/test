using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;

namespace Negosud.ViewModels.Dashboard
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly ArticleService _articleService;
        private readonly SupplierService _supplierService;

        public ObservableCollection<ElementDashboardViewModel> ElementsDashboard { get; private set; }

        private IRelayCommand? _navigateToListingPurchasesCommand;

        public DashboardViewModel()
        {
            _articleService = new ArticleService();
            _supplierService = new SupplierService();
            ElementsDashboard = new ObservableCollection<ElementDashboardViewModel>();

            _ = LoadDataAsync();
        }

        public IRelayCommand NavigateToListingPurchasesCommand => _navigateToListingPurchasesCommand ??= new RelayCommand<object>(_ =>
        {
            if (System.Windows.Application.Current.MainWindow?.DataContext is MainViewModel mainViewModel) mainViewModel.NavigateToListingPurchasesCommand.Execute(null);

        });

        private async Task LoadDataAsync()
        {
            try
            {
                var articlesForRestocking = await _articleService.GetArticlesForRestockingAsync();

                foreach (var article in articlesForRestocking)
                {
                    ElementDashboardViewModel elementVM = new(article, _supplierService, NavigateToListingPurchasesCommand);
                    ElementsDashboard.Add(elementVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des articles à réapprovisionner : {ex.Message}");
            }
        }
    }
}
