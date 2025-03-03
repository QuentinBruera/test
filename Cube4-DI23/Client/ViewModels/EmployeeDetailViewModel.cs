using Client.Services;
using Client.Utils;
using Model.Dto;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class EmployeeDetailViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private readonly SiteService _siteService;
        private readonly DepartmentService _departmentService;
        private readonly NavigationService _navigationService;

        private int _employeeId;
        private EmployeeDto? _employee;
        private SiteDto? _site;
        private DepartmentDto? _department;
        private bool _isLoading;

        public EmployeeDto? Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public SiteDto? Site
        {
            get => _site;
            set
            {
                _site = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SiteCity));
            }
        }

        public DepartmentDto? Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DepartmentName));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        // Propriétés formattées pour l'affichage
        public string FullName => Employee != null ? $"{Employee.FirstName} {Employee.LastName.ToUpper()}" : string.Empty;
        public string SiteCity => Site != null ? Site.City : string.Empty;
        public string DepartmentName => Department != null ? Department.Name : string.Empty;

        // Commandes
        public RelayCommand BackCommand { get; }

        public EmployeeDetailViewModel(int employeeId)
        {
            _employeeId = employeeId;

            // Initialisation des services
            var apiService = ApiService.Instance;
            _employeeService = new EmployeeService(apiService.HttpClient);
            _siteService = new SiteService(apiService.HttpClient);
            _departmentService = new DepartmentService(apiService.HttpClient);
            _navigationService = NavigationService.Instance;

            // Initialisation des commandes
            BackCommand = new RelayCommand(OnBackCommand);

            // Chargement des données
            _ = LoadEmployeeDataAsync();
        }

        private async Task LoadEmployeeDataAsync()
        {
            try
            {
                IsLoading = true;

                // Chargement des données de l'employé
                Employee = await _employeeService.GetEmployee(_employeeId);

                if (Employee != null)
                {
                    // Chargement du site associé
                    Site = await _siteService.GetSite(Employee.SiteId);

                    // Chargement du département associé
                    Department = await _departmentService.GetDepartment(Employee.DepartmentId);
                }
                else
                {
                    MessageBox.Show("Impossible de trouver les informations de l'employé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnBackCommand(object? parameter)
        {
            // Utiliser le service de navigation pour revenir à la page précédente
            _navigationService.GoBack();
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}