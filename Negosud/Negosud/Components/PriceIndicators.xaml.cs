using System.Windows;
using System.Windows.Controls;

namespace Negosud.Components
{
    public partial class PriceIndicators : UserControl
    {
        public PriceIndicators()
        {
            InitializeComponent();
        }

        public decimal TotalWithoutTaxes
        {
            get => (decimal)GetValue(TotalWithoutTaxesProperty);
            set => SetValue(TotalWithoutTaxesProperty, value);
        }

        public static readonly DependencyProperty TotalWithoutTaxesProperty = DependencyProperty.Register("TotalWithoutTaxes", typeof(decimal), typeof(PriceIndicators), new PropertyMetadata(0m));

        public decimal TotalTVA
        {
            get => (decimal)GetValue(TotalTVAProperty);
            set => SetValue(TotalTVAProperty, value);
        }

        public static readonly DependencyProperty TotalTVAProperty = DependencyProperty.Register("TotalTVA", typeof(decimal), typeof(PriceIndicators), new PropertyMetadata(0m));

        public decimal TotalWithTaxes
        {
            get => (decimal)GetValue(TotalWithTaxesProperty);
            set => SetValue(TotalWithTaxesProperty, value);
        }

        public static readonly DependencyProperty TotalWithTaxesProperty = DependencyProperty.Register("TotalWithTaxes", typeof(decimal), typeof(PriceIndicators), new PropertyMetadata(0m));
    }
}
