using Negosud.ViewModels.Purchases;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class CreatePurchaseView : UserControl
    {
        public CreatePurchaseView()
        {
            InitializeComponent();
            this.DataContext = new CreatePurchaseViewModel();
        }
    }
}
