using Negosud.ViewModels.Customers;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class CustomersView : UserControl
    {
        public CustomersView()
        {
            InitializeComponent();
            this.DataContext = new CustomersViewModel();
        }
    }
}
