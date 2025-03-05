using Negosud.ViewModels.Articles;
using System.Windows.Controls;
using System.Windows.Input;

namespace Negosud.Views
{
    public partial class CreateArticleView : UserControl
    {
        public CreateArticleView(ICommand navigateToArticlesCommand)
        {
            InitializeComponent();
            this.DataContext = new CreateArticleViewModel(navigateToArticlesCommand);
        }
    }
}
