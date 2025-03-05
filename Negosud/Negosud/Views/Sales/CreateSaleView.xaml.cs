using Negosud.ViewModels.Sales;
using System.Windows.Controls;

namespace Negosud.Views.Sales
{
    public partial class CreateSaleView : UserControl
    {
        public CreateSaleView()
        {
            InitializeComponent();
            this.DataContext = new CreateSaleViewModel();
        }
    }
}
