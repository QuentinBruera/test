using Negosud.ViewModels.Purchases;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class ListingPurchasesView : UserControl
    {
        public ListingPurchasesView()
        {
            InitializeComponent();
            this.DataContext = new ListingPurchasesViewModel();
        }
    }
}
