using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace Negosud.ViewModels.Articles
{
    public class ArticleViewModel : BaseViewModel
    {
        private readonly ArticleService _articleService;
        private readonly SupplierService _supplierService;
        private readonly FamilyService _familyService;

        private string _supplierName = "";
        private string _familyName = "";

        public ArticleDto Article { get; }

        public Func<Task>? RefreshArticlesAction { get; set; }
        private IAsyncRelayCommand? _deleteCommand;

        public ArticleViewModel(ArticleDto article, ArticleService articleService, SupplierService supplierService, FamilyService familyService)
        {
            Article = article;
            _articleService = articleService;
            _supplierService = supplierService;
            _familyService = familyService;

            _ = LoadSupplierNameAsync();
            _ = LoadFamilyNameAsync();
        }

        public string SupplierName
        {
            get => _supplierName;
            private set
            {
                _supplierName = value;
                OnPropertyChanged();
            }
        }

        public string FamilyName
        {
            get => _familyName;
            private set
            {
                _familyName = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadSupplierNameAsync()
        {
            if (Article.SupplierId > 0)
            {
                SupplierDto? supplier = await _supplierService.GetSupplierByIdAsync(Article.SupplierId);
                SupplierName = supplier?.Name ?? "";
            }
            else
            {
                SupplierName = "";
            }
        }

        private async Task LoadFamilyNameAsync()
        {
            if (Article.FamilyId > 0)
            {
                FamilyDto? family = await _familyService.GetFamilyByIdAsync(Article.FamilyId);
                FamilyName = family?.Name ?? "";
            }
            else
            {
                FamilyName = "";
            }
        }
    }
}
