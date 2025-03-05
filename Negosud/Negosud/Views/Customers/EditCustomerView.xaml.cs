using Negosud.ViewModels.Customers;
using System.Windows.Controls;
using System.Windows.Input;


namespace Negosud.Views
{
    public partial class EditCustomerView : UserControl
    {
        public EditCustomerView(int customerId, ICommand navigateToCustomersCommand)
        {
            InitializeComponent();
            this.DataContext = new EditCustomerViewModel(customerId, navigateToCustomersCommand);
        }
    }
}
