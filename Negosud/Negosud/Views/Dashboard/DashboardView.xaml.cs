using Negosud.ViewModels.Dashboard;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}
