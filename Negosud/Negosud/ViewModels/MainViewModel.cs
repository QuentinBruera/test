using Negosud.Views;
using Negosud.Views.Articles;
using Negosud.Views.Sales;
using System.Windows;
using System.Windows.Input;

namespace Negosud.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private bool _isAdminMode;
        private readonly Stack<object> _viewHistory = new();
        private object _currentView = new DashboardView();
        private string _activeViewName = "Dashboard";

        public bool IsAdminMode
        {
            get => _isAdminMode;
            set
            {
                _isAdminMode = value;
                OnPropertyChanged();
            }
        }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != null) _viewHistory.Push(_currentView);
                
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string ActiveViewName
        {
            get => _activeViewName;
            set
            {
                _activeViewName = value;
                OnPropertyChanged();
            }
        }

        // Dashboard
        public RelayCommand NavigateToDashboardCommand { get; }

        public RelayCommand NavigateBackCommand { get; }

        // Articles
        public RelayCommand NavigateToArticlesCommand { get; }
        public RelayCommand NavigateToListingArticlesCommand { get; }
        public RelayCommand NavigateToCreateArticleCommand { get; }
        public RelayCommand NavigateToEditArticleCommand { get; }

        // Families
        public RelayCommand NavigateToListingFamiliesCommand { get; }
        public RelayCommand NavigateToCreateFamilyCommand { get; }

        // Customers
        public RelayCommand NavigateToCustomersCommand { get; }
        public RelayCommand NavigateToCreateCustomerCommand { get; }
        public RelayCommand NavigateToEditCustomerCommand { get; }

        // Purchases
        public RelayCommand NavigateToPurchasesCommand { get; }
        public RelayCommand NavigateToListingPurchasesCommand { get; }
        public RelayCommand NavigateToCreatePurchaseCommand { get; }
        public RelayCommand NavigateToViewPurchaseCommand { get; }

        // Receptions
        public RelayCommand NavigateToListingReceptionsCommand { get; }

        // Suppliers
        public RelayCommand NavigateToSuppliersCommand { get; }
        public RelayCommand NavigateToCreateSupplierCommand { get; }
        public RelayCommand NavigateToEditSupplierCommand { get; }

        // Inventories
        public RelayCommand NavigateToInventoriesCommand { get; }
        public RelayCommand NavigateToCreateInventoryCommand { get; }
        public RelayCommand NavigateToViewInventoryCommand { get; }

        // Stock
        public RelayCommand NavigateToStockCommand { get; }

        // Sales
        public RelayCommand NavigateToSalesCommand { get; }
        public RelayCommand NavigateToCreateSaleCommand { get; }
        public RelayCommand NavigateToViewSaleCommand { get; }

        public MainViewModel()
        {
            // Initialisation du mode Admin
            IsAdminMode = false;

            // Initialisation des commandes
            NavigateBackCommand = new RelayCommand(o =>
            {
                if (_viewHistory.Count > 0) CurrentView = _viewHistory.Pop(); 
                
            }, o => _viewHistory.Count > 0);

            // Dashboard
            NavigateToDashboardCommand = new RelayCommand(o =>
            {
                CurrentView = new DashboardView();
                ActiveViewName = "Dashboard";
            });

            // Articles
            NavigateToArticlesCommand = new RelayCommand(o =>
            {
                CurrentView = new ArticlesView();
                ActiveViewName = "Articles";
            });

            NavigateToListingArticlesCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingArticlesView();
                ActiveViewName = "ListingArticles";
            });

            NavigateToCreateArticleCommand = new RelayCommand(o =>
            {
                CurrentView = new CreateArticleView(NavigateToArticlesCommand);
                ActiveViewName = "CreateArticle";
            });

            NavigateToEditArticleCommand = new RelayCommand(o =>
            {
                if (o is int articleId)
                {
                    CurrentView = new EditArticleView(articleId, NavigateToArticlesCommand);
                    ActiveViewName = "EditArticle";
                }
            });

            // Families
            NavigateToListingFamiliesCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingFamiliesView();
                ActiveViewName = "ListingFamilies";
            });

            NavigateToCreateFamilyCommand = new RelayCommand(o =>
            {
                CurrentView = new CreateFamilyView();
                ActiveViewName = "CreateFamily";
            });


            // Customers
            NavigateToCustomersCommand = new RelayCommand(o =>
            {
                CurrentView = new CustomersView();
                ActiveViewName = "Clients";
            });

            NavigateToCreateCustomerCommand = new RelayCommand(o =>
            {
                CurrentView = new CreateCustomerView(NavigateToCustomersCommand);
                ActiveViewName = "CreateCustomer";
            });

            NavigateToEditCustomerCommand = new RelayCommand(o =>
            {
                if (o is int customerId)
                {
                    CurrentView = new EditCustomerView(customerId, NavigateToCustomersCommand);
                    ActiveViewName = "EditCustomer";
                }
            });

            // Purchases
            NavigateToPurchasesCommand = new RelayCommand(o =>
            {
                CurrentView = new PurchasesView();
                ActiveViewName = "Purchases";
            });

            NavigateToListingPurchasesCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingPurchasesView();
                ActiveViewName = "ListingPurchases";
            });

            NavigateToCreatePurchaseCommand = new RelayCommand(o =>
            {
                CurrentView = new CreatePurchaseView();
                ActiveViewName = "CreatePurchase";
            });

            NavigateToViewPurchaseCommand = new RelayCommand(o =>
            {
                if (o is int purchaseId)
                {
                    CurrentView = new ViewPurchaseView(purchaseId);
                    ActiveViewName = "ViewPurchase";
                }
            });

            // Receptions
            NavigateToListingReceptionsCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingReceptionsView();
                ActiveViewName = "ListingReceptions";
            });

            // Suppliers
            NavigateToSuppliersCommand = new RelayCommand(o =>
            {
                CurrentView = new SuppliersView();
                ActiveViewName = "Suppliers";
            });

            NavigateToCreateSupplierCommand = new RelayCommand(o =>
            {
                CurrentView = new CreateSupplierView(NavigateToSuppliersCommand);
                ActiveViewName = "CreateSupplier";
            });

            NavigateToEditSupplierCommand = new RelayCommand(o =>
            {
                if (o is int supplierId)
                {
                    CurrentView = new EditSupplierView(supplierId, NavigateToSuppliersCommand);
                    ActiveViewName = "EditSupplier";
                }
            });

            // Inventories
            NavigateToInventoriesCommand = new RelayCommand(o =>
            {
                CurrentView = new InventoriesView();
                ActiveViewName = "Inventories";
            });

            NavigateToCreateInventoryCommand = new RelayCommand(o =>
            {
                CurrentView = new CreateInventoryView();
                ActiveViewName = "CreateInventory";
            });

            NavigateToViewInventoryCommand = new RelayCommand(o =>
            {
                if (o is int inventoryId)
                {
                    CurrentView = new ViewInventoryView(inventoryId);
                    ActiveViewName = "ViewInventory";
                }
            });

            // Stock
            NavigateToStockCommand = new RelayCommand(o =>
            {
                CurrentView = new StockView();
                ActiveViewName = "Stock";
            });

            // Sales
            NavigateToSalesCommand = new RelayCommand(o =>
            {
                CurrentView = new SalesView();
                ActiveViewName = "Sales";
            });

            NavigateToCreateSaleCommand = new RelayCommand(o =>
            {
                CurrentView = new CreateSaleView();
                ActiveViewName = "CreateSale";
            });

            NavigateToViewSaleCommand = new RelayCommand(o =>
            {
                if (o is int saleId)
                {
                    CurrentView = new ViewSaleView(saleId);
                    ActiveViewName = "ViewSale";
                }
            });

        }

        // Toggle Admin Mode
        private void ToggleAdminMode()
        {
            IsAdminMode = !IsAdminMode;
            if (IsAdminMode) MessageBox.Show("Vous êtes passé en mode Admin", "Mode Admin Activé", MessageBoxButton.OK, MessageBoxImage.Information);
            else MessageBox.Show("Vous n'êtes plus en mode Admin", "Mode Admin Désactivé", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Handle keyboard input
        public void HandleKeyDown(KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.A) ToggleAdminMode(); 
        }
    }
}
