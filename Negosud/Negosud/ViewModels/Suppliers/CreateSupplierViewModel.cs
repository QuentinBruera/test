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

namespace Negosud.ViewModels
{
    public class CreateSupplierViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;
        private readonly ICommand _navigateToSuppliersCommand;

        private string _supplierName = string.Empty;
        private string _supplierStreet = string.Empty;
        private string _supplierZipCode = string.Empty;
        private string _supplierCity = string.Empty;
        private string _supplierCellPhone = string.Empty;
        private string _supplierLandline = string.Empty;

        public ICommand ValidateCommand { get; }

        public CreateSupplierViewModel(ICommand navigateToSuppliersCommand)
        {
            _supplierService = new SupplierService();
            _navigateToSuppliersCommand = navigateToSuppliersCommand;
            ValidateCommand = new RelayCommand<object>(async param => await ValidateFormAsync());
        }

        public string SupplierName
        {
            get => _supplierName;
            set
            {
                _supplierName = value;
                OnPropertyChanged();
            }
        }


        public string SupplierStreet
        {
            get => _supplierStreet;
            set
            {
                _supplierStreet = value;
                OnPropertyChanged();
            }
        }

        public string SupplierZipCode
        {
            get => _supplierZipCode;
            set
            {
                _supplierZipCode = value;
                OnPropertyChanged();
            }
        }

        public string SupplierCity
        {
            get => _supplierCity;
            set
            {
                _supplierCity = value;
                OnPropertyChanged();
            }
        }

        public string SupplierCellPhone
        {
            get => _supplierCellPhone;
            set
            {
                _supplierCellPhone = value;
                OnPropertyChanged();
            }
        }

        public string SupplierLandline
        {
            get => _supplierLandline;
            set
            {
                _supplierLandline = value;
                OnPropertyChanged();
            }
        }

        private async Task ValidateFormAsync()
        {
            try
            {
                CreateUpdateSupplierRequest? request = new()
                {
                    Name = SupplierName,
                    Address = SupplierStreet,
                    ZipCode = SupplierZipCode,
                    City = SupplierCity,
                    CellPhoneNumber = SupplierCellPhone,
                    LandlineNumber = SupplierLandline
                };

                int supplierId = await _supplierService.CreateSupplierAsync(request);

                if (supplierId > 0)
                {
                    MessageBox.Show("Fournisseur créé avec succès !",
                        "Succès",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    _navigateToSuppliersCommand.Execute(null);

                }
                else
                {
                    MessageBox.Show("Erreur lors de la création du fournisseur. Veuillez réessayer.",
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