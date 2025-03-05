using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace Negosud.ViewModels.Inventories
{
    public class InventoryViewModel : BaseViewModel
    {
        private readonly InventoryService _inventoryService;

        public InventoryDto Inventory { get; }
        public Action? RefreshInventoriesAction { get; set; }

        private IAsyncRelayCommand? _deleteCommand;

        public InventoryViewModel(InventoryDto inventory, InventoryService inventoryService)
        {
            Inventory = inventory;
            _inventoryService = inventoryService;
        }

        public IAsyncRelayCommand DeleteCommand => _deleteCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                bool success = await _inventoryService.DeleteInventory(Inventory.Id);
                if (success)
                {
                    Console.WriteLine($"Inventory {Inventory.Id} successfully deleted.");
                    RefreshInventoriesAction?.Invoke();
                }
                else
                {
                    Console.WriteLine($"Purchase {Inventory.Id} not found or could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting purchase {Inventory.Id}: {ex.Message}");
            }
        });
    }
}
