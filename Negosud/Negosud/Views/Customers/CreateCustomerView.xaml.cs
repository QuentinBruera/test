using Negosud.ViewModels.Customers;
using System.Windows.Controls;
using System.Windows.Input;


namespace Negosud.Views
{
    public partial class CreateCustomerView : UserControl
    {
        public CreateCustomerView(ICommand navigateToCustomersCommand)
        {
            InitializeComponent();
            this.DataContext = new CreateCustomerViewModel(navigateToCustomersCommand);
        }
    }
}
