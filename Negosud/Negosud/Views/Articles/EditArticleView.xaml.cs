using Negosud.ViewModels.Articles;
using System.Windows.Controls;
using System.Windows.Input;


namespace Negosud.Views.Articles
{
    public partial class EditArticleView : UserControl
    {
        public EditArticleView(int articleId, ICommand navigateToArticlesCommand)
        {
            InitializeComponent();
            this.DataContext = new EditArticleViewModel(articleId, navigateToArticlesCommand);
        }
    }
}
