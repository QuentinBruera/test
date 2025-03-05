using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negosud.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NegosudModel.Request;
using System.Windows;
using NegosudModel.Dto;
using System.Collections.ObjectModel;

namespace Negosud.ViewModels.Articles
{
    public class CreateArticleViewModel : BaseViewModel
    {
        private readonly ArticleService _articleService;
        private readonly ICommand _navigateToArticlesCommand;

        public CreateArticleViewModel(ICommand navigateToArticlesCommand)
        {
            _articleService = new ArticleService();
            _navigateToArticlesCommand = navigateToArticlesCommand;
        }
    }
}
