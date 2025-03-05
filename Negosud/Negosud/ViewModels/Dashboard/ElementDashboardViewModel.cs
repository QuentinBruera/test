using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using System.Windows.Input;

namespace Negosud.ViewModels.Dashboard
{
    public class ElementDashboardViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;
        private readonly PurchaseService _purchaseService;
        private readonly ICommand _navigateToListingPurchasesCommand;
        private string _supplierName;

        public ArticleDto Article { get; }
        public string SupplierName
        {
            get => _supplierName;
            private set
            {
                _supplierName = value;
                OnPropertyChanged();
            }
        }

        private IAsyncRelayCommand? _createPurchaseRestockingAndRedirectionCommand;

        public ElementDashboardViewModel(
            ArticleDto article,
            SupplierService supplierService,
            ICommand navigateToListingPurchasesCommand)
        {
            Article = article;
            _supplierService = supplierService;
            _purchaseService = new PurchaseService();
            _navigateToListingPurchasesCommand = navigateToListingPurchasesCommand;
            _supplierName = string.Empty;

            _ = LoadSupplierNameAsync();
        }

        private async Task LoadSupplierNameAsync()
        {
            try
            {
                SupplierDto? supplier = await _supplierService.GetSupplierByIdAsync(Article.SupplierId);
                SupplierName = supplier?.Name ?? "Inconnu";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération du fournisseur pour l'article {Article.Id}: {ex.Message}");
                SupplierName = "Erreur";
            }
        }

        public IAsyncRelayCommand CreatePurchaseRestockingAndRedirectionCommand => _createPurchaseRestockingAndRedirectionCommand ??= new AsyncRelayCommand(async () =>
        {
            try
            {
                if (Article.Id > 0)
                {
                    int purchaseId = await _purchaseService.CreatePurchaseRestockingAsync(Article.Id);

                    if (purchaseId > 0)
                    {
                        Console.WriteLine($"Commande de réapprovisionnement créée avec succès (ID: {purchaseId}) pour l'article {Article.Name}.");
                        _navigateToListingPurchasesCommand.Execute(null);
                    }
                    else
                    {
                        Console.WriteLine($"Échec de la création de la commande pour l'article {Article.Name}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création de la commande de réapprovisionnement pour l'article {Article.Id} : {ex.Message}");
            }
        });
    }
}
