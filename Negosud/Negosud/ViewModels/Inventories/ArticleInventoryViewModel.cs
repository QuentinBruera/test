using System.ComponentModel;
using NegosudModel.Entities;

namespace Negosud.ViewModels.Inventories
{
    public class ArticleInventoryViewModel : BaseViewModel
    {
        private readonly ArticleInventory _articleInventory;
        private bool _isValidated;

        public ArticleInventoryViewModel(ArticleInventory articleInventory)
        {
            _articleInventory = articleInventory;
            _isValidated = false;
        }

        public int ArticleId => _articleInventory.ArticleId;

        public string ArticleName => _articleInventory.Article?.Name ?? "";

        public int QuantityBefore
        {
            get => _articleInventory.QuantityBefore;
            set
            {
                if (_articleInventory.QuantityBefore != value)
                {
                    _articleInventory.QuantityBefore = value;
                    OnPropertyChanged();
                }
            }
        }

        public int QuantityAfter
        {
            get => _articleInventory.QuantityAfter;
            set
            {
                if (_articleInventory.QuantityAfter != value)
                {
                    _articleInventory.QuantityAfter = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsValidated
        {
            get => _isValidated;
            set
            {
                if (_isValidated != value)
                {
                    _isValidated = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
