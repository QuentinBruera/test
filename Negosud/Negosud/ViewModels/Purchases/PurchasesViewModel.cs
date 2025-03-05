using System.Windows.Input;
using Negosud.Views;

namespace Negosud.ViewModels.Purchases
{
    public class PurchasesViewModel : BaseViewModel
    {
        private object _currentView = new ListingPurchasesView();
        private string _activeTab = "Commandes";

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

        public ICommand NavigateToListingPurchasesCommand { get; }
        public ICommand NavigateToListingReceptionsCommand { get; }

        public PurchasesViewModel()
        {
            NavigateToListingPurchasesCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingPurchasesView();
                ActiveTab = "Commandes";
            });

            NavigateToListingReceptionsCommand = new RelayCommand(o =>
            {
                CurrentView = new ListingReceptionsView();
                ActiveTab = "Réceptions";
            });
        }
    }
}
