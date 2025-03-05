using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace Negosud.ViewModels
{
    public class EditSupplierViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;
        private readonly ICommand _navigateToSuppliersCommand;
        private int _supplierId;

        private string _supplierName = string.Empty;
        private string _supplierStreet = string.Empty;
        private string _supplierZipCode = string.Empty;
        private string _supplierCity = string.Empty;
        private string _supplierCellPhone = string.Empty;
        private string _supplierLandline = string.Empty;

        public ICommand ValidateCommand { get; }
        public ICommand DeleteCommand { get; }

        public EditSupplierViewModel(int supplierId, ICommand navigateToSuppliersCommand)
        {
            _supplierService = new SupplierService();
            _navigateToSuppliersCommand = navigateToSuppliersCommand;
            _supplierId = supplierId;

            ValidateCommand = new RelayCommand<object>(async _ => await SaveSupplierAsync());
            DeleteCommand = new RelayCommand<object>(async _ => await DeleteSupplierAsync());

            _ = LoadSupplierAsync();
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

        private async Task LoadSupplierAsync()
        {
            try
            {
                SupplierDto? supplier = await _supplierService.GetSupplierByIdAsync(_supplierId);

                if (supplier != null)
                {
                    SupplierName = supplier.Name ?? string.Empty;
                    SupplierStreet = supplier.Address ?? string.Empty;
                    SupplierZipCode = supplier.ZipCode ?? string.Empty;
                    SupplierCity = supplier.City ?? string.Empty;
                    SupplierCellPhone = supplier.CellPhoneNumber ?? string.Empty;
                    SupplierLandline = supplier.LandlineNumber ?? string.Empty;
                }
                else
                {
                    MessageBox.Show("Impossible de charger les informations du fournisseur.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement du fournisseur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SaveSupplierAsync()
        {
            try
            {
                CreateUpdateSupplierRequest request = new()
                {
                    Name = SupplierName,
                    Address = SupplierStreet,
                    ZipCode = SupplierZipCode,
                    City = SupplierCity,
                    CellPhoneNumber = SupplierCellPhone,
                    LandlineNumber = SupplierLandline
                };


                bool success = await _supplierService.UpdateSupplierAsync(_supplierId, request);

                if (success)
                {
                    MessageBox.Show("Fournisseur mis à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    _navigateToSuppliersCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la mise à jour du fournisseur.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteSupplierAsync()
        {
            try
            {
                var result = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce fournisseur ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool success = await _supplierService.DeleteSupplierAsync(_supplierId);

                    if (success)
                    {
                        MessageBox.Show("Fournisseur supprimé avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                        _navigateToSuppliersCommand.Execute(null);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la suppression du fournisseur.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du fournisseur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
