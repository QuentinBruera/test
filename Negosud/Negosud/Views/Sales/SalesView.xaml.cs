using Negosud.ViewModels.Sales;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
            this.DataContext = new SalesViewModel();
        }
    }
}
