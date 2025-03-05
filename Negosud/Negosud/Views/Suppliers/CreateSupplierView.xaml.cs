using Negosud.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;


namespace Negosud.Views
{
    public partial class CreateSupplierView : UserControl
    {
        public CreateSupplierView(ICommand navigateToSuppliersCommand)
        {
            InitializeComponent();
            this.DataContext = new CreateSupplierViewModel(navigateToSuppliersCommand);
        }
    }
}
