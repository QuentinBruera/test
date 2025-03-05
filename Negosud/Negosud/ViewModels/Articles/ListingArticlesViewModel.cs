using NegosudModel.Dto;
using System.Collections.ObjectModel;
using Negosud.Services;

namespace Negosud.ViewModels.Articles
{
    public class ListingArticlesViewModel : BaseViewModel
    {
        private readonly ArticleService _articleService;
        private readonly SupplierService _supplierService;
        private readonly FamilyService _familyService;

        private ObservableCollection<ArticleViewModel> _articles;

        public ListingArticlesViewModel()
        {
            _articleService = new ArticleService();
            _articles = new ObservableCollection<ArticleViewModel>();
            _supplierService = new SupplierService();
            _familyService = new FamilyService();
            _ = LoadDataAsync();
        }

        public ObservableCollection<ArticleViewModel> Articles
        {
            get => _articles;
            set
            {
                _articles = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                IEnumerable<ArticleDto>? articlesFromApi = await _articleService.GetArticlesAsync();

                foreach (ArticleDto? article in articlesFromApi)
                {
                    ArticleViewModel? articleVM = new(article, _articleService, _supplierService, _familyService)
                    {
                        RefreshArticlesAction = async () => await RefreshArticlesAsync()
                    };
                    _articles.Add(articleVM);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public async Task RefreshArticlesAsync()
        {
            Articles.Clear();
            await LoadDataAsync();
        }
    }
}
