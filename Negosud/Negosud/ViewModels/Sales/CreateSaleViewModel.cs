using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using Negosud.ViewModels.Purchases;
using NegosudModel.Dto;
using NegosudModel.Entities;
using NegosudModel.Request;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Negosud.ViewModels.Sales
{
    public class CreateSaleViewModel : BaseViewModel
    {
        private DateTime? _saleDate;
        private readonly CustomerService _customerService;
        private readonly ArticleService _articleService;
        private readonly SaleService _saleService;

        private ObservableCollection<CustomerDto> _customers = [];
        private ObservableCollection<ArticleDto> _articles = [];
        private ObservableCollection<ArticleOrderViewModel> _articleOrders = [];
        private CustomerDto? _selectedCustomer;
        private ArticleDto? _selectedArticle;

        public ICommand RemoveArticleOrder {  get; }
        public ICommand ValidateCommand { get; }
        public decimal TotalWithoutTaxes => ArticleOrders.Sum(a => (decimal)a.TotalWithoutTaxes);
        public decimal TotalTVA => ArticleOrders.Sum(a => (decimal)(a.TotalWithTaxes - a.TotalWithoutTaxes));
        public decimal TotalWithTaxes => ArticleOrders.Sum(a => (decimal)a.TotalWithTaxes);

        public CreateSaleViewModel()
        {
            _customerService = new CustomerService();
            _articleService = new ArticleService();
            _saleService = new SaleService();
            _ = LoadCustomersAsync();
            _ = LoadArticlesAsync();
            RemoveArticleOrder = new RelayCommand<ArticleOrderViewModel>(RemoveArticle);
            ValidateCommand = new RelayCommand<object>(async param => await ValidateFormAsync());
        }

        public DateTime? SaleDate
        {
            get => _saleDate;
            set
            {
                if (value.HasValue)
                {
                    _saleDate = value.Value.Date + DateTime.Now.TimeOfDay;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CustomerDto> Customers
        {
            get => _customers;
            set
            {
                _customers = value ?? [];
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
            }
        }

        public CustomerDto? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
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

        private async Task LoadCustomersAsync()
        {
            IEnumerable<CustomerDto> customersFromApi = await _customerService.GetCustomersAsync();
            Customers = new ObservableCollection<CustomerDto>(customersFromApi);
        }

        private async Task LoadArticlesAsync()
        {
            IEnumerable<ArticleDto> articlesFromApi = await _articleService.GetArticlesAsync();
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
                OnPropertyChanged(nameof(TotalWithoutTaxes));
                OnPropertyChanged(nameof(TotalTVA));
                OnPropertyChanged(nameof(TotalWithTaxes));
            }
        }

        private async Task ValidateFormAsync()
        {
            // Vérification des données obligatoires
            if (SelectedCustomer == null || !ArticleOrders.Any())
            {
                MessageBox.Show("Veuillez sélectionner un client et ajouter au moins un article.",
                    "Validation échouée",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                CreateSaleRequest request = new CreateSaleRequest
                {
                    CustomerId = SelectedCustomer.Id,
                    Date = SaleDate ?? DateTime.Now,
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
                            SupplierId = 0,
                            FamilyId = 0
                        },
                        Quantity = ao.Quantity
                    }).ToList()
                };

                int saleId = await _saleService.CreateSaleWithArticlesAsync(request);

                if (saleId > 0)
                {
                    if (Application.Current.MainWindow.DataContext is MainViewModel mainViewModel) mainViewModel.NavigateToSalesCommand.Execute(null);

                }
                else
                {
                    MessageBox.Show("Erreur lors de la création de la vente. Veuillez réessayer.",
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
