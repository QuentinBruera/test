using System.ComponentModel;
using System.Runtime.CompilerServices;
using NegosudModel.Entities;

namespace Negosud.ViewModels.Purchases
{
    public class ArticleOrderViewModel : BaseViewModel
    {
        private readonly ArticleOrder _articleOrder;

        public ArticleOrderViewModel(ArticleOrder articleOrder)
        {
            _articleOrder = articleOrder;
        }

        public int ArticleId => _articleOrder.ArticleId;

        public string ArticleName => _articleOrder.Article?.Name ?? "";

        public int Quantity
        {
            get => _articleOrder.Quantity;
            set
            {
                if (_articleOrder.Quantity != value)
                {
                    _articleOrder.Quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalWithoutTaxes));
                    OnPropertyChanged(nameof(TotalWithTaxes));
                }
            }
        }

        public double UnitPrice => _articleOrder.UnitPrice;

        public double TVA => _articleOrder.TVA;

        public double TotalWithoutTaxes => UnitPrice * Quantity;

        public double TotalWithTaxes => TotalWithoutTaxes * (1 + TVA);
    }
}

