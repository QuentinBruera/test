using Negosud.ViewModels.Receptions;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class ListingReceptionsView : UserControl
    {
        public ListingReceptionsView()
        {
            InitializeComponent();
            DataContext = new ListingReceptionsViewModel();
        }
    }
}
