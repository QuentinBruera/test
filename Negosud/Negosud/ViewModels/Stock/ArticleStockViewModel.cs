using CommunityToolkit.Mvvm.Input;
using Negosud.Services;
using NegosudModel.Dto;
using NegosudModel.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Negosud.ViewModels.Stock
{
    public class ArticleStockViewModel : BaseViewModel
    {
        public ArticleDetailsDto Article { get; set; }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RowDetailsVisibility));
                }
            }
        }

        private ObservableCollection<ReasonDto> _reasons = new ObservableCollection<ReasonDto>();
        public ObservableCollection<ReasonDto> Reasons
        {
            get => _reasons;
            set
            {
                _reasons = value;
                OnPropertyChanged();
            }
        }

        private ReasonDto _selectedReason;
        public ReasonDto SelectedReason
        {
            get => _selectedReason;
            set
            {
                _selectedReason = value;
                OnPropertyChanged();
            }
        }

        public Visibility RowDetailsVisibility => IsEditing ? Visibility.Visible : Visibility.Collapsed;

        public ICommand CancelEditingCommand { get; }
        public ICommand TriggerActionCommand { get; }

        private readonly StockMovementService _stockMovementService;
        private readonly ArticleService _articleService;

        public ArticleStockViewModel(ArticleDetailsDto article, ObservableCollection<ReasonDto> reasonDtos)
        {
            Article = article;
            IsEditing = false;
            NewQuantity = "0";
            Reasons = reasonDtos;

            CancelEditingCommand = new RelayCommand(CancelEditing);
            _stockMovementService = new StockMovementService();
            _articleService = new ArticleService();
            TriggerActionCommand = new RelayCommand(async () => await TriggerAction());
        }
        private void CancelEditing()
        {
            IsEditing = false;
            NewQuantity = "0";
        }

        private string _newQuantity;
        public string NewQuantity
        {
            get => _newQuantity;
            set
            {
                // Vérifie si la valeur est un nombre
                if (int.TryParse(value, out int numericValue))
                {
                    // Si le nombre est dans les limites, on l'assigne
                    if (numericValue >= 0 && numericValue < Article.Quantity)
                    {
                        _newQuantity = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        // Si hors limites, réinitialise à "0" et affiche un message
                        MessageBox.Show($"Veuillez saisir un nombre entre 0 et {Article.Quantity - 1}", "Valeur invalide", MessageBoxButton.OK, MessageBoxImage.Warning);
                        _newQuantity = "0";
                        OnPropertyChanged();
                    }
                }
                else
                {
                    // Si ce n'est pas un nombre, réinitialise à "0"
                    MessageBox.Show("Veuillez saisir uniquement des chiffres.", "Valeur invalide", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _newQuantity = "0";
                    OnPropertyChanged();
                }
            }
        }
        private async Task TriggerAction()
        {
            // Validation des champs avant de faire l'appel
            if (SelectedReason == null || string.IsNullOrEmpty(NewQuantity) || !int.TryParse(NewQuantity, out int quantity))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement avant de soumettre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int stockMovementQuantity = Article.Quantity - quantity;

            // Préparer l'objet StockMovementDto
            StockMovementDto stockMovement = new()
            {
                Date = DateTime.UtcNow, // Date générée au moment de l'envoi
                Quantity = -stockMovementQuantity,
                ArticleId = Article.Id,
                ReasonId = SelectedReason.Id,
                Reason = new ReasonDto
                {
                    Id = SelectedReason.Id,
                    Name = SelectedReason.Name,
                    Color = SelectedReason.Color
                }
            };

            try
            {
                // Envoyer la requête via le service
                StockMovementDto result = await _stockMovementService.PostStockMovement(stockMovement);

                // Vérifier la réponse et mettre à jour la quantité d'article
                if (result != null && result.Id > 0)
                {
                    // Mise à jour de la quantité de l'article
                    Article.Quantity = quantity;

                    // Mise à jour de l'article dans la base de données
                    bool isUpdated = await UpdateArticle();

                    MessageBox.Show("Mouvement de stock ajouté avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    CancelEditing(); // Réinitialise les champs après succès
                }
                else
                {
                    MessageBox.Show("Une erreur est survenue lors de l'ajout du mouvement de stock.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Task<bool> UpdateArticle()
        {
            ArticleDto articleDto = new()
            {
                Id = Article.Id,
                Name = Article.Name,
                Quantity = Article.Quantity,
                SupplierId = Article.SupplierId,
                TVA = Article.TVA,
                Description = Article.Description,
                UnitPrice = Article.UnitPrice,
                MinimumQuantity = Article.MinimumQuantity,
                IsActive = Article.IsActive,
                FamilyId = Article.FamilyId,
            };

            return _articleService.UpdateArticleAsync(articleDto);
        }
    }
}