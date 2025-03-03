using Client.ViewModels;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Logique d'interaction pour HomePageControl.xaml
    /// </summary>
    public partial class HomePageControl : UserControl
    {
        public HomePageControl()
        {
            InitializeComponent();
            DataContext = new HomePageViewModel();
        }
    }
}