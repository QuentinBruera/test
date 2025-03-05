using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negosud.Services;

namespace Negosud.ViewModels.Inventories
{
    public class InventoriesViewModel : BaseViewModel
    {
        private readonly InventoryService _inventoryService;
        private ObservableCollection<InventoryViewModel> _inventories;

        public InventoriesViewModel()
        {
            _inventoryService = new InventoryService();
            _inventories = new ObservableCollection<InventoryViewModel>();
            _ = LoadDataAsync();
        }

        public ObservableCollection<InventoryViewModel> Inventories
        {
            get => _inventories;
            set
            {
                _inventories = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var inventoriesFromApi = await _inventoryService.GetInventoriesAsync();

                foreach (var inventory in inventoriesFromApi)
                {
                    InventoryViewModel inventoryVM = new(inventory, _inventoryService)
                    {
                        RefreshInventoriesAction = async () => await RefreshInventoriesAsync()
                    };
                    _inventories.Add(inventoryVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading inventories: {ex.Message}");
            }
        }

        public async Task RefreshInventoriesAsync()
        {
            Inventories.Clear();
            await LoadDataAsync();
        }
    }
}
