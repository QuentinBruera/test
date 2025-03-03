using Client.Services;
using Client.Utils;
using Client.Views;
using Model.Request;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly LoginApiService _loginApiService;
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        // Événement déclenché quand l'authentification réussit
        public event EventHandler? AuthenticationSucceeded;

        public MainWindowViewModel()
        {
            _loginApiService = new LoginApiService();
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                IsLoading = true;
                bool isAuthenticated = await _loginApiService.LoginAsync(
                    new LoginRequest
                    {
                        Email = "admin@gmail.com",
                        Password = "Admin1!"
                    },
                    useCookies: true,
                    useSessionCookies: true
                );

                if (!isAuthenticated)
                {
                    MessageBox.Show("Échec de l'authentification.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                    return;
                }

                // Déclencher l'événement d'authentification réussie
                AuthenticationSucceeded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'authentification: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}