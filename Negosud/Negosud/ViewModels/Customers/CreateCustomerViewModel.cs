using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace Negosud.ViewModels.Customers
{
    public class CreateCustomerViewModel : BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly ICommand _navigateToCustomersCommand;

        private string _customerName = string.Empty;
        private string _customerFirstName = string.Empty;
        private DateTime? _customerBirthDate = DateTime.Today;
        private string _customerStreet = string.Empty;
        private string _customerZipCode = string.Empty;
        private string _customerCity = string.Empty;
        private string _customerCellPhone = string.Empty;
        private string _customerLandline = string.Empty;

        public ICommand ValidateCommand { get; }

        public CreateCustomerViewModel(ICommand navigateToCustomersCommand)
        {
            _customerService = new CustomerService();
            _navigateToCustomersCommand = navigateToCustomersCommand;
            ValidateCommand = new RelayCommand<object>(async param => await ValidateFormAsync());
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

        private async Task ValidateFormAsync()
        {
            try
            {
                CreateUpdateCustomerRequest? request = new()
                {
                    Name = CustomerName,
                    FirstName = CustomerFirstName,
                    DateOfBirth = CustomerBirthDate.HasValue ? DateOnly.FromDateTime(CustomerBirthDate.Value) : DateOnly.FromDateTime(DateTime.Now),
                    Address = CustomerStreet,
                    ZipCode = CustomerZipCode,
                    City = CustomerCity,
                    CellPhoneNumber = CustomerCellPhone,
                    LandlineNumber = CustomerLandline
                };

                int customerId = await _customerService.CreateCustomerAsync(request);

                if (customerId > 0)
                {
                    MessageBox.Show("Client créé avec succès !",
                        "Succès",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    _navigateToCustomersCommand.Execute(null);

                }
                else
                {
                    MessageBox.Show("Erreur lors de la création du client. Veuillez réessayer.",
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
