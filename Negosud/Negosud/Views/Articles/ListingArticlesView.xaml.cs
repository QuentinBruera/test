using Negosud.ViewModels.Articles;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class ListingArticlesView : UserControl
    {
        public ListingArticlesView()
        {
            InitializeComponent();
            this.DataContext = new ListingArticlesViewModel();
        }
    }
}
