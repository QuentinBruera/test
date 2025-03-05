using Negosud.ViewModels.Sales;
using System.Windows.Controls;

namespace Negosud.Views.Sales
{
    public partial class ViewSaleView : UserControl
    {
        public ViewSaleView(int saleId)
        {
            InitializeComponent();
            DataContext = new ViewSaleViewModel(saleId);
        }
    }
}
