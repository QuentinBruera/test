using Negosud.ViewModels.Suppliers;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class SuppliersView : UserControl
    {
        public SuppliersView()
        {
            InitializeComponent();
            this.DataContext = new SuppliersViewModel();
        }
    }
}
