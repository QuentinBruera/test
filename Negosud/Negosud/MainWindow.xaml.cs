using Negosud.Services;
using Negosud.ViewModels;
using NegosudModel.DatasTest;
using System.Windows;
using System.Windows.Input;

namespace Negosud
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitialSeed.SeedDatas();
            LoginToApplicationAsAdmin();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
            this.KeyDown += OnKeyDownHandler;
        }

        private async void LoginToApplicationAsAdmin()
        {
            LoginService? loginService = new();
            string? token = await loginService.LoginAsAdmin(useCookies: true, useSessionCookies: true);
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
           _viewModel.HandleKeyDown(e);
        }
    }
}