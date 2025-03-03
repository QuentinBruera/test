using Client.Services;
using Client.ViewModels;
using Client.Views;
using Model.InitDatas;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly NavigationService _navigationService;

        public MainWindow()
        {
            InitializeComponent();

            // Initialiser les données de test si nécessaire
            InitSeed.Seed();

            // Initialiser le service de navigation
            _navigationService = NavigationService.Instance;
            _navigationService.Initialize(MainContent);

            // Initialiser le ViewModel
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;

            // S'abonner à l'événement d'authentification réussie
            _viewModel.AuthenticationSucceeded += ViewModel_AuthenticationSucceeded;
        }

        private void ViewModel_AuthenticationSucceeded(object? sender, System.EventArgs e)
        {
            // Quand l'authentification réussit, naviguer vers la page d'accueil
            _navigationService.NavigateTo(new HomePageControl());
        }

        protected override void OnClosed(System.EventArgs e)
        {
            // Nettoyage
            _viewModel.AuthenticationSucceeded -= ViewModel_AuthenticationSucceeded;
            base.OnClosed(e);
        }
    }
}