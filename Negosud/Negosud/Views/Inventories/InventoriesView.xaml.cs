using Negosud.ViewModels.Inventories;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class InventoriesView : UserControl
    {
        public InventoriesView()
        {
            InitializeComponent();
            DataContext = new InventoriesViewModel();
        }
    }
}
