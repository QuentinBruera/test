using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using Negosud.ViewModels.Purchases;

namespace Negosud.ViewModels.Receptions
{
    public class ListingReceptionsViewModel : BaseViewModel
    {
        private readonly PurchaseService _purchaseService;
        private readonly SupplierService _supplierService;
        private readonly StatusService _statusService;

        private ObservableCollection<PurchaseViewModel> _receptions;

        public ListingReceptionsViewModel()
        {
            _purchaseService = new PurchaseService();
            _supplierService = new SupplierService();
            _statusService = new StatusService();
            _receptions = new ObservableCollection<PurchaseViewModel>();
            _ = LoadDataAsync();
        }

        public ObservableCollection<PurchaseViewModel> Receptions
        {
            get => _receptions;
            set
            {
                _receptions = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var receptionsFromApi = await _purchaseService.GetReceptions();

                foreach (var reception in receptionsFromApi)
                {
                    PurchaseViewModel? receptionVM = new(reception, _supplierService, _purchaseService, _statusService, "Reception")
                    {
                        RefreshPurchasesAction = async () => await RefreshReceptionsAsync()
                    };
                    _receptions.Add(receptionVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading receptions : {ex.Message}");
            }
        }

        public async Task RefreshReceptionsAsync()
        {
            Receptions.Clear();
            await LoadDataAsync();
        }
    }
}
