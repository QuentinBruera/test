using System.Collections.ObjectModel;
using Negosud.Services;
using Negosud.ViewModels.Purchases;
using NegosudModel.Entities;

namespace Negosud.ViewModels.Sales
{
    public class SalesViewModel : BaseViewModel
    {
        private readonly SaleService _saleService;
        private readonly CustomerService _customerService;
        private readonly StatusService _statusService;

        private ObservableCollection<SaleViewModel> _sales;

        public SalesViewModel()
        {
            _saleService = new SaleService();
            _customerService = new CustomerService();
            _statusService = new StatusService();
            _sales = new ObservableCollection<SaleViewModel>();
            _ = LoadDataAsync();
        }

        public ObservableCollection<SaleViewModel> Sales
        {
            get => _sales;
            set
            {
                _sales = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var salesFromApi = await _saleService.GetSales();

                foreach (var sales in salesFromApi)
                {
                    SaleViewModel? saleVM = new(sales, _customerService, _saleService, _statusService, "Sale")
                    {
                        RefreshSalesAction = async () => await RefreshSalesAsync()
                    };
                    _sales.Add(saleVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data : {ex.Message}");
            }
        }

        public async Task RefreshSalesAsync()
        {
            Sales.Clear();
            await LoadDataAsync();
        }
    }
}
