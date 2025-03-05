using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using Negosud.Utils;
using NegosudModel.Dto;

namespace Negosud.ViewModels.Purchases
{
    public class PurchaseViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;
        private readonly PurchaseService _purchaseService;
        private readonly StatusService _statusService;
        private readonly string _context;

        private string _supplierName = "";
        private string _translatedStatusName = "";
        private string _rawStatusName = "";
        private decimal _totalWithTaxes;
        private Brush _badgeBackground;

        public PurchaseDto Purchase { get; }
        public Action? RefreshPurchasesAction { get; set; }

        private IAsyncRelayCommand? _cancelCommand;
        private IAsyncRelayCommand? _receiveCommand;
        private IAsyncRelayCommand? _deleteCommand;

        public PurchaseViewModel(PurchaseDto purchase, SupplierService supplierService, PurchaseService purchaseService, StatusService statusService, string context = "Purchase")
        {
            Purchase = purchase;
            _supplierService = supplierService;
            _purchaseService = purchaseService;
            _statusService = statusService;
            _context = context;
            _badgeBackground = Brushes.Transparent;

            _ = LoadSupplierNameAsync();
            _ = LoadTotalWithTaxesAsync();
            _ = LoadStatusAsync();
        }

        public string SupplierName
        {
            get => _supplierName;
            private set
            {
                _supplierName = value;
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

        private async Task LoadSupplierNameAsync()
        {
            if (Purchase.SupplierId > 0)
            {
                SupplierDto? supplier = await _supplierService.GetSupplierByIdAsync(Purchase.SupplierId);
                SupplierName = supplier?.Name ?? "";
            }
            else
            {
                SupplierName = "";
            }
        }

        private async Task LoadTotalWithTaxesAsync()
        {
            try
            {
                PurchaseDto? purchaseDetails = await _purchaseService.GetPurchaseByIdAsync(Purchase.Id);
                if (purchaseDetails != null)
                {
                    TotalWithTaxes = (decimal)purchaseDetails.TotalWithTaxes;
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
            if (Purchase.StatusId > 0)
            {
                StatusDto? status = await _statusService.GetStatusByIdAsync(Purchase.StatusId);
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
            Purchase.StatusId = newStatusId;
            RawStatusName = newRawStatusName;
            BadgeBackground = BadgeColorMapper.GetColorForStatus(newRawStatusName);
            TranslatedStatusName = StatusTranslator.Translate(_context, newRawStatusName);
        }

        public IAsyncRelayCommand CancelCommand => _cancelCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _purchaseService.CancelPurchase(Purchase.Id);
                if (success)
                {
                    UpdateStatus(newStatusId: 10, newRawStatusName: "Cancelled");
                    Console.WriteLine($"Purchase {Purchase.Id} successfully cancelled.");
                    RefreshPurchasesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Purchase {Purchase.Id} not found or could not be cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while canceling purchase {Purchase.Id}: {ex.Message}");
            }
        });

        public IAsyncRelayCommand ReceiveCommand => _receiveCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _purchaseService.ReceivePurchase(Purchase.Id);
                if (success)
                {
                    UpdateStatus(newStatusId: 9, newRawStatusName: "Done");
                    Console.WriteLine($"Purchase {Purchase.Id} successfully marked as received.");
                    RefreshPurchasesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Purchase {Purchase.Id} not found or could not be marked as received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while receiving purchase {Purchase.Id}: {ex.Message}");
            }
        });

        public IAsyncRelayCommand DeleteCommand => _deleteCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _purchaseService.DeletePurchase(Purchase.Id);
                if (success)
                {
                    Console.WriteLine($"Purchase {Purchase.Id} successfully deleted.");
                    RefreshPurchasesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Purchase {Purchase.Id} not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting purchase {Purchase.Id}: {ex.Message}");
            }
        });
    }
}
