using Client.Services;
using Client.Utils;
using Client.Views;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeService _employeeService;
        private readonly SiteService _siteService;
        private readonly DepartmentService _departmentService;
        private readonly NavigationService _navigationService;

        private ObservableCollection<EmployeeDto> _employees = new();
        private ObservableCollection<EmployeeDto> _filteredEmployees = new();
        private ObservableCollection<SiteDto> _sites = new();
        private ObservableCollection<DepartmentDto> _departments = new();

        private SiteDto? _selectedSite;
        private DepartmentDto? _selectedDepartment;
        private string _searchText = string.Empty;
        private bool _isLoading;

        public ObservableCollection<EmployeeDto> FilteredEmployees
        {
            get => _filteredEmployees;
            set
            {
                _filteredEmployees = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SiteDto> Sites
        {
            get => _sites;
            set
            {
                _sites = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DepartmentDto> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged();
            }
        }

        public SiteDto? SelectedSite
        {
            get => _selectedSite;
            set
            {
                _selectedSite = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        public DepartmentDto? SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilters();
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

        public RelayCommand EmployeeSelectedCommand { get; }
        public RelayCommand ClearFiltersCommand { get; }

        public HomePageViewModel()
        {
            // Utiliser l'instance singleton de ApiService
            var apiService = ApiService.Instance;

            // Initialiser les services
            _employeeService = new EmployeeService(apiService.HttpClient);
            _siteService = new SiteService(apiService.HttpClient);
            _departmentService = new DepartmentService(apiService.HttpClient);
            _navigationService = NavigationService.Instance;

            // Initialiser les commandes
            EmployeeSelectedCommand = new RelayCommand(OnEmployeeSelected);
            ClearFiltersCommand = new RelayCommand(OnClearFilters);

            // Charger les données
            _ = LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;

                // Charger les sites
                var sites = await _siteService.GetSites();
                Sites = new ObservableCollection<SiteDto>(sites);

                // Charger les départements
                var departments = await _departmentService.GetDepartments();
                Departments = new ObservableCollection<DepartmentDto>(departments);

                // Charger les employés
                var employees = await _employeeService.GetEmployees();
                _employees = new ObservableCollection<EmployeeDto>(employees);
                FilteredEmployees = new ObservableCollection<EmployeeDto>(_employees);
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

        private void ApplyFilters()
        {
            // Appliquer tous les filtres (recherche, site et département)
            var filtered = _employees.AsEnumerable();

            // Filtre par texte de recherche
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string searchLower = SearchText.ToLower();
                filtered = filtered.Where(e =>
                    e.LastName.ToLower().Contains(searchLower) ||
                    e.FirstName.ToLower().Contains(searchLower));
            }

            // Filtre par site
            if (SelectedSite != null)
            {
                filtered = filtered.Where(e => e.SiteId == SelectedSite.Id);
            }

            // Filtre par département
            if (SelectedDepartment != null)
            {
                filtered = filtered.Where(e => e.DepartmentId == SelectedDepartment.Id);
            }

            // Mise à jour de la collection filtrée
            FilteredEmployees = new ObservableCollection<EmployeeDto>(filtered);
        }

        private void OnClearFilters(object? parameter)
        {
            SearchText = string.Empty;
            SelectedSite = null;
            SelectedDepartment = null;

            // Réinitialiser la liste filtrée
            FilteredEmployees = new ObservableCollection<EmployeeDto>(_employees);
        }

        private void OnEmployeeSelected(object? parameter)
        {
            if (parameter is EmployeeDto selectedEmployee)
            {
                // Création du contrôle de détail d'employé
                var detailControl = new EmployeeDetailControl(selectedEmployee.Id);

                // Utilisation du service de navigation pour naviguer vers ce contrôle
                _navigationService.NavigateTo(detailControl);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}