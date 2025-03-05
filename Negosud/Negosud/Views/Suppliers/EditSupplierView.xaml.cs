using Negosud.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;


namespace Negosud.Views
{
    public partial class EditSupplierView : UserControl
    {
        public EditSupplierView(int supplierId, ICommand navigateToSuppliersCommand)
        {
            InitializeComponent();
            this.DataContext = new EditSupplierViewModel(supplierId, navigateToSuppliersCommand);
        }
    }
}
