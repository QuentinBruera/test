using Negosud.ViewModels.Inventories;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class ViewInventoryView : UserControl
    {
        public ViewInventoryView(int inventoryId)
        {
            InitializeComponent();
            DataContext = new ViewInventoryViewModel(inventoryId);
        }
    }
}
