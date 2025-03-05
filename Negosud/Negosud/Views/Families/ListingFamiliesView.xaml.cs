using Negosud.ViewModels.Families;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class ListingFamiliesView : UserControl
    {
        public ListingFamiliesView()
        {
            InitializeComponent();
            this.DataContext = new ListingFamiliesViewModel();
        }
    }
}
