using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace Negosud.ViewModels.Customers
{
    public class EditCustomerViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly ICommand _navigateToCustomersCommand;
        private int _customerId;

        private string _customerName = string.Empty;
        private string _customerFirstName = string.Empty;
        private DateTime? _customerBirthDate = DateTime.Today;
        private string _customerStreet = string.Empty;
        private string _customerZipCode = string.Empty;
        private string _customerCity = string.Empty;
        private string _customerCellPhone = string.Empty;
        private string _customerLandline = string.Empty;

        public ICommand ValidateCommand { get; }
        public ICommand DeleteCommand { get; }

        public EditCustomerViewModel(int customerId, ICommand navigateToCustomersCommand)
        {
            _customerService = new CustomerService();
            _navigateToCustomersCommand = navigateToCustomersCommand;
            _customerId = customerId;

            ValidateCommand = new RelayCommand<object>(async _ => await SaveCustomerAsync());
            DeleteCommand = new RelayCommand<object>(async _ => await DeleteCustomerAsync());

            _ = LoadCustomerAsync();
        }

        public string CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                OnPropertyChanged();
            }
        }

        public string CustomerFirstName
        {
            get => _customerFirstName;
            set
            {
                _customerFirstName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? CustomerBirthDate
        {
            get => _customerBirthDate;
            set
            {
                _customerBirthDate = value;
                OnPropertyChanged();
            }
        }

        public string CustomerStreet
        {
            get => _customerStreet;
            set
            {
                _customerStreet = value;
                OnPropertyChanged();
            }
        }

        public string CustomerZipCode
        {
            get => _customerZipCode;
            set
            {
                _customerZipCode = value;
                OnPropertyChanged();
            }
        }

        public string CustomerCity
        {
            get => _customerCity;
            set
            {
                _customerCity = value;
                OnPropertyChanged();
            }
        }

        public string CustomerCellPhone
        {
            get => _customerCellPhone;
            set
            {
                _customerCellPhone = value;
                OnPropertyChanged();
            }
        }

        public string CustomerLandline
        {
            get => _customerLandline;
            set
            {
                _customerLandline = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadCustomerAsync()
        {
            try
            {
                CustomerDto? customer = await _customerService.GetCustomerByIdAsync(_customerId);

                if (customer != null)
                {
                    CustomerName = customer.Name ?? string.Empty;
                    CustomerFirstName = customer.FirstName ?? string.Empty;
                    CustomerBirthDate = customer.DateOfBirth.ToDateTime(TimeOnly.MinValue);
                    CustomerStreet = customer.Address ?? string.Empty;
                    CustomerZipCode = customer.ZipCode ?? string.Empty;
                    CustomerCity = customer.City ?? string.Empty;
                    CustomerCellPhone = customer.CellPhoneNumber ?? string.Empty;
                    CustomerLandline = customer.LandlineNumber ?? string.Empty;
                }
                else
                {
                    MessageBox.Show("Impossible de charger les informations du client.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du client : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SaveCustomerAsync()
        {
            try
            {
                CreateUpdateCustomerRequest request = new()
                {
                    Name = CustomerName,
                    FirstName = CustomerFirstName,
                    DateOfBirth = CustomerBirthDate.HasValue ? DateOnly.FromDateTime(CustomerBirthDate.Value) : DateOnly.FromDateTime(DateTime.Today),
                    Address = CustomerStreet,
                    ZipCode = CustomerZipCode,
                    City = CustomerCity,
                    CellPhoneNumber = CustomerCellPhone,
                    LandlineNumber = CustomerLandline
                };


                bool success = await _customerService.UpdateCustomerAsync(_customerId, request);

                if (success)
                {
                    MessageBox.Show("Client mis à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    _navigateToCustomersCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour du client.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteCustomerAsync()
        {
            try
            {
                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce client ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool success = await _customerService.DeleteCustomerAsync(_customerId);

                    if (success)
                    {
                        MessageBox.Show("Client supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                        _navigateToCustomersCommand.Execute(null);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression du client.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du client : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
