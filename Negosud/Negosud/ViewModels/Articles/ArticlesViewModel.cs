using CommunityToolkit.Mvvm.ComponentModel;
using Negosud.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Negosud.ViewModels.Articles
{
    public class ArticlesViewModel : BaseViewModel
    {
        private object _currentView = new ListingArticlesView();
        private string _activeTab = "Articles";

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string ActiveTab
        {
            get => _activeTab;
            set
            {
                _activeTab = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToListingArticlesCommand { get; }
        public ICommand NavigateToListingFamiliesCommand { get; }

        public ArticlesViewModel()
        {
            NavigateToListingArticlesCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingArticlesView();
                ActiveTab = "Articles";
            });

            NavigateToListingFamiliesCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingFamiliesView();
                ActiveTab = "Familles";
            });
        }
    }
}
