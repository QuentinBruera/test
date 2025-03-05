using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Negosud.Services;
using NegosudModel.Dto;

namespace Negosud.ViewModels.Customers
{
    public class CustomersViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;

        private ObservableCollection<CustomerViewModel> _customers;

        public CustomersViewModel()
        {
            _customerService = new CustomerService();
            _customers = new ObservableCollection<CustomerViewModel>();
            _ = LoadDataAsync();
        }

        public ObservableCollection<CustomerViewModel> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                IEnumerable<CustomerDto>? customersFromApi = await _customerService.GetCustomersAsync();

                foreach (CustomerDto? customer in customersFromApi)
                {
                    CustomerViewModel? customerVM = new(customer, _customerService)
                    {
                        RefreshCustomersAction = async () => await RefreshCustomersAsync()
                    };
                    _customers.Add(customerVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public async Task RefreshCustomersAsync()
        {
            Customers.Clear();
            await LoadDataAsync();
        }
    }
}
