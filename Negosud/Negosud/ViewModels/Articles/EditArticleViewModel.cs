using Negosud.Services;
using System.Windows.Input;

namespace Negosud.ViewModels.Articles
{
    public class EditArticleViewModel : BaseViewModel
    {
        private readonly ArticleService _articleService;
        private readonly ICommand _navigateToArticlesCommand;
        private int _articleId;

        public EditArticleViewModel(int articleId, ICommand navigateToArticlesCommand)
        {
            _articleService = new ArticleService();
            _navigateToArticlesCommand = navigateToArticlesCommand;
            _articleId = articleId;
        }
    }
}
