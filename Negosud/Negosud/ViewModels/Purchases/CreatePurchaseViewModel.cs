using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;

namespace Negosud.ViewModels.Purchases
{
    public class CreatePurchaseViewModel : BaseViewModel
    {
        private DateTime? _deliveryDate;
        private readonly SupplierService _supplierService;
        private readonly ArticleService _articleService;
        private readonly PurchaseService _purchaseService;

        private ObservableCollection<SupplierDto> _suppliers = [];
        private ObservableCollection<ArticleDto> _articles = [];
        private ObservableCollection<ArticleOrderViewModel> _articleOrders = [];
        private SupplierDto? _selectedSupplier;
        private ArticleDto? _selectedArticle;

        private bool _canChangeSupplier = true;
        public ICommand RemoveArticleOrder { get; }
        public ICommand ValidateCommand { get; }

        public CreatePurchaseViewModel()
        {
            _supplierService = new SupplierService();
            _articleService = new ArticleService();
            _purchaseService = new PurchaseService();
            _ = LoadSuppliersAsync();
            RemoveArticleOrder = new RelayCommand<ArticleOrderViewModel>(RemoveArticle);
            ValidateCommand = new RelayCommand<object>(async param => await ValidateFormAsync());
        }

        public DateTime? DeliveryDate
        {
            get => _deliveryDate;
            set
            {
                if (value.HasValue)
                {
                    _deliveryDate = value.Value.Date + DateTime.Now.TimeOfDay;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SupplierDto> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value ?? [];
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ArticleDto> Articles
        {
            get => _articles;
            set
            {
                _articles = value ?? [];
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ArticleOrderViewModel> ArticleOrders
        {
            get => _articleOrders;
            set
            {
                _articleOrders = value ?? [];
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalWithoutTaxes));
                OnPropertyChanged(nameof(TotalTVA));
                OnPropertyChanged(nameof(TotalWithTaxes));
                UpdateCanChangeSupplier();
            }
        }

        public bool CanChangeSupplier
        {
            get => _canChangeSupplier;
            private set
            {
                _canChangeSupplier = value;
                OnPropertyChanged();
            }
        }

        public SupplierDto? SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                if (!CanChangeSupplier && value != _selectedSupplier) return;
                _selectedSupplier = value;
                OnPropertyChanged();
                _ = LoadArticlesAsync();
            }
        }

        public ArticleDto? SelectedArticle
        {
            get => _selectedArticle;
            set
            {
                _selectedArticle = value;
                OnPropertyChanged();
                if (_selectedArticle != null) AddArticleToGrid(_selectedArticle);
            }
        }

        private async Task LoadSuppliersAsync()
        {
            IEnumerable<SupplierDto> suppliersFromApi = await _supplierService.GetSuppliersAsync();
            Suppliers = new ObservableCollection<SupplierDto>(suppliersFromApi);
        }

        private async Task LoadArticlesAsync()
        {
            if (SelectedSupplier == null) return;

            IEnumerable<ArticleDto> articlesFromApi = await _articleService.GetArticlesBySupplierIdAsync(SelectedSupplier.Id);
            Articles.Clear();

            foreach (ArticleDto article in articlesFromApi)
            {
                Articles.Add(article);
            }
        }

        private void AddArticleToGrid(ArticleDto selectedArticle)
        {
            if (ArticleOrders.Any(ao => ao.ArticleId == selectedArticle.Id)) return;

            ArticleOrder articleOrder = new ArticleOrder
            {
                ArticleId = selectedArticle.Id,
                Article = selectedArticle.ToEntity(),
                Quantity = 0,
                UnitPrice = selectedArticle.UnitPrice,
                TVA = selectedArticle.TVA
            };

            ArticleOrderViewModel viewModel = new ArticleOrderViewModel(articleOrder);
            viewModel.PropertyChanged += ArticleOrder_PropertyChanged;

            ArticleOrders.Add(viewModel);
            UpdateCanChangeSupplier();
            OnPropertyChanged(nameof(TotalWithoutTaxes));
            OnPropertyChanged(nameof(TotalTVA));
            OnPropertyChanged(nameof(TotalWithTaxes));
        }

        private void ArticleOrder_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ArticleOrderViewModel.Quantity) ||
                e.PropertyName == nameof(ArticleOrderViewModel.TotalWithoutTaxes) ||
                e.PropertyName == nameof(ArticleOrderViewModel.TotalWithTaxes))
            {
                OnPropertyChanged(nameof(TotalWithoutTaxes));
                OnPropertyChanged(nameof(TotalTVA));
                OnPropertyChanged(nameof(TotalWithTaxes));
            }
        }

        private void RemoveArticle(ArticleOrderViewModel? articleOrder)
        {
            if (articleOrder == null) return;
            if (ArticleOrders.Contains(articleOrder))
            {
                articleOrder.PropertyChanged -= ArticleOrder_PropertyChanged;
                ArticleOrders.Remove(articleOrder);
                UpdateCanChangeSupplier();
                OnPropertyChanged(nameof(TotalWithoutTaxes));
                OnPropertyChanged(nameof(TotalTVA));
                OnPropertyChanged(nameof(TotalWithTaxes));
            }
        }

        private void UpdateCanChangeSupplier()
        {
            CanChangeSupplier = !ArticleOrders.Any();
        }

        public decimal TotalWithoutTaxes => ArticleOrders.Sum(a => (decimal)a.TotalWithoutTaxes);
        public decimal TotalTVA => ArticleOrders.Sum(a => (decimal)(a.TotalWithTaxes - a.TotalWithoutTaxes));
        public decimal TotalWithTaxes => ArticleOrders.Sum(a => (decimal)a.TotalWithTaxes);

        private async Task ValidateFormAsync()
        {
            // Vérification des données obligatoires
            if (SelectedSupplier == null || !ArticleOrders.Any())
            {
                MessageBox.Show("Veuillez sélectionner un fournisseur et ajouter au moins un article.",
                    "Validation échouée",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                CreatePurchaseRequest request = new CreatePurchaseRequest
                {
                    SupplierId = SelectedSupplier.Id,
                    Date = DeliveryDate ?? DateTime.Now,
                    ArticleQuantities = ArticleOrders.Select(ao => new ArticleQuantity
                    {
                        Article = new ArticleDto
                        {
                            Id = ao.ArticleId,
                            Name = ao.ArticleName,
                            TVA = ao.TVA,
                            Description = "",
                            UnitPrice = ao.UnitPrice,
                            Quantity = 0,
                            MinimumQuantity = 0,
                            IsActive = true,
                            SupplierId = SelectedSupplier.Id,
                            FamilyId = 0
                        },
                        Quantity = ao.Quantity
                    }).ToList()
                };

                int purchaseId = await _purchaseService.CreatePurchaseWithArticlesAsync(request);

                if (purchaseId > 0)
                {
                    if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel) mainViewModel.NavigateToPurchasesCommand.Execute(null);

                }
                else
                {
                    MessageBox.Show("Erreur lors de la création de la commande. Veuillez réessayer.",
                        "Erreur",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue : {ex.Message}",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}