using NegosudModel.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negosud.Services;
using NegosudModel.Entities;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using Negosud.Views;

namespace Negosud.ViewModels.Stock
{
    internal class StockViewModel : BaseViewModel
    {
        private readonly ArticleService _articleService;
        private readonly SupplierService _supplierService;
        private readonly ReasonService _reasonService;
        private readonly StockMovementService _stockMovementService;

        private ObservableCollection<ArticleStockViewModel> _articlesDetails = new ObservableCollection<ArticleStockViewModel>();
        public ObservableCollection<ArticleStockViewModel> ArticlesDetails
        {
            get => _articlesDetails;
            set
            {
                _articlesDetails = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReasonDto> _reasons = new ObservableCollection<ReasonDto>();
        public ObservableCollection<ReasonDto> Reasons
        {
            get => _reasons;
            set
            {
                _reasons = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartEditingCommand { get; }
        public ICommand TriggerViewCommand { get; }

        public StockViewModel()
        {
            _articleService = new ArticleService();
            _supplierService = new SupplierService();
            _reasonService = new ReasonService();
            _stockMovementService = new StockMovementService();

            StartEditingCommand = new RelayCommand<ArticleStockViewModel>(StartEditing);
            TriggerViewCommand = new RelayCommand<int>(async (articleId) => await TriggerViewAsync(articleId));


            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await LoadReasonsAsync();
            await LoadArticlesDetailsAsync();
        }

        private async Task LoadReasonsAsync()
        {
            try
            {
                IEnumerable<ReasonDto> reasons = await _reasonService.GetReasonsAsync();
                Reasons = new ObservableCollection<ReasonDto>(reasons);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des raisons : {ex.Message}");
            }
        }

        private async Task LoadArticlesDetailsAsync()
        {
            try
            {
                IEnumerable<ArticleDetailsDto> articlesDetails = await _articleService.GetArticlesDetailsAsync();

                articlesDetails = filterByQuantity(new ObservableCollection<ArticleDetailsDto>(articlesDetails), 0);

                // Pour chaque articlesDetails je veux les ajouters à la liste ArticlesDetails
                foreach (ArticleDetailsDto articleDetails in articlesDetails)
                {
                    ArticlesDetails.Add(new ArticleStockViewModel(articleDetails, Reasons));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des détails des articles : {ex.Message}");
            }
        }

        private ObservableCollection<ArticleDetailsDto> filterByQuantity(ObservableCollection<ArticleDetailsDto> articles, int quantity)
        {
            return new ObservableCollection<ArticleDetailsDto>(articles.Where(a => a.Quantity > quantity));
        }

        private void StartEditing(ArticleStockViewModel article)
        {
            if (article != null)
            {
                // Basculer le mode édition pour l'article sélectionné
                article.IsEditing = !article.IsEditing;
                OnPropertyChanged(nameof(ArticlesDetails));
            }
        }

        private async Task TriggerViewAsync(object parameter)
        {
            if (parameter is int articleId)
            {
                try
                {
                    // Récupérer les mouvements de stock pour cet ID
                    IEnumerable<StockMovementDto> stockMovements = await _stockMovementService.GetStockMovementsByArticleId(articleId);

                    ObservableCollection<StockMovementViewModel> stockMovementsList = new();

                    foreach (StockMovementDto stockMovement in stockMovements)
                    {
                        // Rechercher la raison correspondant à ReasonId
                        ReasonDto reason = Reasons.FirstOrDefault(r => r.Id == stockMovement.ReasonId);
                        stockMovementsList.Add(new StockMovementViewModel(stockMovement, reason));
                    }

                    //Afficher le popup
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        StockMovementsView stockMovementsView = new(stockMovementsList);
                        stockMovementsView.ShowDialog();
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la récupération des mouvements de stock : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Paramètre invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
