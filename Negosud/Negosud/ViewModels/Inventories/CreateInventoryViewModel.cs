using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace Negosud.ViewModels.Inventories
{
    public class CreateInventoryViewModel : BaseViewModel
    {
        private DateTime? _inventoryDate;
        private readonly ArticleService _articleService;
        private readonly InventoryService _inventoryService;
        private ObservableCollection<ArticleInventoryViewModel> _articles;

        public ICommand ValidateCommand { get; }

        public CreateInventoryViewModel()
        {
            _articleService = new ArticleService();
            _inventoryService = new InventoryService();
            _articles = new ObservableCollection<ArticleInventoryViewModel>();
            ValidateCommand = new RelayCommand<object>(async param => await ValidateInventoryAsync());

            _ = LoadArticlesAsync();
        }

        public DateTime? InventoryDate
        {
            get => _inventoryDate;
            set
            {
                if (value.HasValue)
                {
                    _inventoryDate = value.Value.Date + DateTime.Now.TimeOfDay;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ArticleInventoryViewModel> Articles
        {
            get => _articles;
            set
            {
                _articles = value ?? [];
                OnPropertyChanged();
            }
        }

        private async Task LoadArticlesAsync()
        {
            try
            {
                IEnumerable<ArticleDto> articlesFromApi = await _articleService.GetArticlesAsync();

                Articles.Clear();

                foreach (var article in articlesFromApi)
                {
                    ArticleInventory articleInventory = new ArticleInventory
                    {
                        ArticleId = article.Id,
                        Article = article.ToEntity(),
                        QuantityBefore = article.Quantity,
                        QuantityAfter = article.Quantity
                    };

                    Articles.Add(new ArticleInventoryViewModel(articleInventory));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des articles : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ValidateInventoryAsync()
        {
            try
            {
                if (InventoryDate == null)
                {
                    MessageBox.Show("Veuillez sélectionner une date d'inventaire.", "Validation échouée", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool allValidated = Articles.All(a => a.QuantityAfter >= 0 && a.IsValidated);
                if (!allValidated)
                {
                    MessageBox.Show("Tous les articles doivent avoir une quantité et être validés pour créer l'inventaire.",
                                    "Validation échouée", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CreateInventoryRequest? request = new()
                {
                    Date = InventoryDate.Value,
                    ArticleInventories = Articles.Select(a => new ArticleInventoryRequest
                    {
                        ArticleId = a.ArticleId,
                        QuantityBefore = a.QuantityBefore,
                        QuantityAfter = a.QuantityAfter
                    }).ToList()
                };

                bool success = await _inventoryService.CreateInventoryAsync(request);

                if (success)
                {
                    if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel) mainViewModel.NavigateToInventoriesCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création de l'inventaire.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
