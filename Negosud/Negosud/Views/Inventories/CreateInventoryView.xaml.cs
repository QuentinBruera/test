using Negosud.ViewModels.Inventories;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class CreateInventoryView : UserControl
    {
        public CreateInventoryView()
        {
            InitializeComponent();
            this.DataContext = new CreateInventoryViewModel();
        }
    }
}
