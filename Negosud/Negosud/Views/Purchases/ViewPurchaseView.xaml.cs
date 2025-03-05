using Negosud.ViewModels.Purchases;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class ViewPurchaseView : UserControl
    {
        public ViewPurchaseView(int purchaseId)
        {
            InitializeComponent();
            DataContext = new ViewPurchaseViewModel(purchaseId);
        }
    }
}
