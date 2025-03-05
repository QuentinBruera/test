using Negosud.ViewModels.Purchases;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;


namespace Negosud.Views
{
    public partial class PurchasesView : UserControl
    {
        public PurchasesView()
        {
            InitializeComponent();
            DataContext = new PurchasesViewModel();

            SetActiveButton(PurchasesButton);
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetButtonColors();
            activeButton.Background = (Brush)Application.Current.Resources["HeaderColor"];
            DropShadowEffect shadowEffect = new DropShadowEffect
            {
                ShadowDepth = 0, 
                BlurRadius = 4,  
                Color = Colors.Black,
                Opacity = 0.25  
            };
            activeButton.Effect = shadowEffect;
        }

        private void OnNavButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            SetActiveButton(clickedButton);
        }

        private void ResetButtonColors()
        {
            PurchasesButton.Background = new SolidColorBrush(Colors.Transparent);
            PurchasesButton.Effect = null;
            ReceptionsButton.Background = new SolidColorBrush(Colors.Transparent);
            ReceptionsButton.Effect = null;
        }
    }
}
