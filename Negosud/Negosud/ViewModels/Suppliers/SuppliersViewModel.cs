using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Negosud.Services;
using NegosudModel.Dto;


namespace Negosud.ViewModels.Suppliers
{
    public class SuppliersViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;

        private ObservableCollection<SupplierViewModel> _suppliers;

        public SuppliersViewModel()
        {
            _supplierService = new SupplierService();
            _suppliers = new ObservableCollection<SupplierViewModel>();
            _ = LoadDataAsync();
        }

        public ObservableCollection<SupplierViewModel> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                IEnumerable<SupplierDto>? suppliersFromApi = await _supplierService.GetSuppliersAsync();

                foreach (SupplierDto? supplier in suppliersFromApi)
                {
                    SupplierViewModel? supplierVM = new(supplier, _supplierService)
                    {
                        RefreshSuppliersAction = async () => await RefreshSuppliersAsync()
                    };
                    _suppliers.Add(supplierVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public async Task RefreshSuppliersAsync()
        {
            Suppliers.Clear();
            await LoadDataAsync();
        }
    }
}