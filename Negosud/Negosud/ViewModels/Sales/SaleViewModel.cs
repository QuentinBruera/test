using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using Negosud.Utils;
using NegosudModel.Dto;
using System.Windows.Media;

namespace Negosud.ViewModels.Sales
{
    public class SaleViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly SaleService _saleService;
        private readonly StatusService _statusService;
        private readonly string _context;

        private string _customerName = "";
        private string _translatedStatusName = "";
        private string _rawStatusName = "";
        private decimal _totalWithTaxes;
        private Brush _badgeBackground;

        public SaleDto Sale { get; }
        public Action? RefreshSalesAction { get; set; }

        private IAsyncRelayCommand? _cancelCommand;
        private IAsyncRelayCommand? _receiveCommand;
        private IAsyncRelayCommand? _deleteCommand;

        public SaleViewModel(SaleDto sale, CustomerService customerService, SaleService saleService, StatusService statusService, string context = "Sale")
        {
            Sale = sale;
            _customerService = customerService;
            _saleService = saleService;
            _statusService = statusService;
            _context = context;
            _badgeBackground = Brushes.Transparent;

            _ = LoadCustomerNameAsync();
            _ = LoadTotalWithTaxesAsync();
            _ = LoadStatusAsync();
        }

        public string CustomerName
        {
            get => _customerName;
            private set
            {
                _customerName = value;
                OnPropertyChanged();
            }
        }

        public string TranslatedStatusName
        {
            get => _translatedStatusName;
            private set
            {
                _translatedStatusName = value;
                OnPropertyChanged();
            }
        }

        public string RawStatusName
        {
            get => _rawStatusName;
            private set
            {
                _rawStatusName = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalWithTaxes
        {
            get => _totalWithTaxes;
            private set
            {
                _totalWithTaxes = value;
                OnPropertyChanged();
            }
        }

        public Brush BadgeBackground
        {
            get => _badgeBackground;
            private set
            {
                _badgeBackground = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadCustomerNameAsync()
        {
            if (Sale.CustomerId > 0)
            {
                CustomerDto? customer = await _customerService.GetCustomerByIdAsync(Sale.CustomerId);
                CustomerName = customer?.FullName ?? "";
            }
            else
            {
                CustomerName = "";
            }
        }

        private async Task LoadTotalWithTaxesAsync()
        {
            try
            {
                SaleDto? saleDetails = await _saleService.GetSaleByIdAsync(Sale.Id);
                if (saleDetails != null)
                {
                    TotalWithTaxes = (decimal)saleDetails.TotalWithTaxes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving the TTC : {ex.Message}");
                TotalWithTaxes = 0;
            }
        }

        private async Task LoadStatusAsync()
        {
            if (Sale.StatusId > 0)
            {
                StatusDto? status = await _statusService.GetStatusByIdAsync(Sale.StatusId);
                RawStatusName = status?.Name ?? "";
                BadgeBackground = BadgeColorMapper.GetColorForStatus(RawStatusName);
                TranslatedStatusName = StatusTranslator.Translate(_context, RawStatusName);
            }
            else
            {
                RawStatusName = "";
                BadgeBackground = BadgeColorMapper.GetColorForStatus(RawStatusName);
                TranslatedStatusName = "";
            }
        }

        public void UpdateStatus(int newStatusId, string newRawStatusName)
        {
            Sale.StatusId = newStatusId;
            RawStatusName = newRawStatusName;
            BadgeBackground = BadgeColorMapper.GetColorForStatus(newRawStatusName);
            TranslatedStatusName = StatusTranslator.Translate(_context, newRawStatusName);
        }

        public IAsyncRelayCommand CancelCommand => _cancelCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _saleService.CancelSale(Sale.Id);
                if (success)
                {
                    UpdateStatus(newStatusId: 10, newRawStatusName: "Cancelled");
                    Console.WriteLine($"Sale {Sale.Id} successfully cancelled.");
                    RefreshSalesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Sale {Sale.Id} not found or could not be cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while canceling sale {Sale.Id}: {ex.Message}");
            }
        });

        public IAsyncRelayCommand ReceiveCommand => _receiveCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _saleService.ReceiveSale(Sale.Id);
                if (success)
                {
                    UpdateStatus(newStatusId: 9, newRawStatusName: "Done");
                    Console.WriteLine($"Sale {Sale.Id} successfully marked as received.");
                    RefreshSalesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Sale {Sale.Id} not found or could not be marked as received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while receiving sale {Sale.Id}: {ex.Message}");
            }
        });

        public IAsyncRelayCommand DeleteCommand => _deleteCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _saleService.DeleteSale(Sale.Id);
                if (success)
                {
                    Console.WriteLine($"Sale {Sale.Id} successfully deleted.");
                    RefreshSalesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Sale {Sale.Id} not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting sale {Sale.Id}: {ex.Message}");
            }
        });
    }
}
