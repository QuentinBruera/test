using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;
using System.Windows;

namespace Negosud.ViewModels
{
    public class SupplierViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;

        public SupplierDto Supplier { get; }

        public Func<Task>? RefreshSuppliersAction { get; set; }
        private IAsyncRelayCommand? _deleteCommand;

        public SupplierViewModel(SupplierDto supplier, SupplierService supplierService)
        {
            Supplier = supplier;
            _supplierService = supplierService;
        }

        public IAsyncRelayCommand DeleteCommand => _deleteCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _supplierService.DeleteSupplierAsync(Supplier.Id);
                if (success)
                {
                    Console.WriteLine($"Supplier {Supplier.Id} successfully deleted.");
                    RefreshSuppliersAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Supplier {Supplier.Id} not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting supplier {Supplier.Id}: {ex.Message}");
            }
        });
    }
}
