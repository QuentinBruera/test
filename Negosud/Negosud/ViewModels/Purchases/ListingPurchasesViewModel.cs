using System.Collections.ObjectModel;
using Negosud.Services;

namespace Negosud.ViewModels.Purchases
{
    public class ListingPurchasesViewModel : BaseViewModel
    {
        private readonly PurchaseService _purchaseService;
        private readonly SupplierService _supplierService;
        private readonly StatusService _statusService;

        private ObservableCollection<PurchaseViewModel> _purchases;

        public ListingPurchasesViewModel()
        {
            _purchaseService = new PurchaseService();
            _supplierService = new SupplierService();
            _statusService = new StatusService();
            _purchases = new ObservableCollection<PurchaseViewModel>();
            _ = LoadDataAsync();
        }

        public ObservableCollection<PurchaseViewModel> Purchases
        {
            get => _purchases;
            set
            {
                _purchases = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var purchasesFromApi = await _purchaseService.GetPurchases();

                foreach (var purchase in purchasesFromApi)
                {
                    PurchaseViewModel? purchaseVM = new(purchase, _supplierService, _purchaseService, _statusService, "Purchase")
                    {
                        RefreshPurchasesAction = async () => await RefreshPurchasesAsync()
                    };
                    _purchases.Add(purchaseVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data : {ex.Message}");
            }
        }

        public async Task RefreshPurchasesAsync()
        {
            Purchases.Clear();
            await LoadDataAsync();
        }
    }
}
