using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using Negosud.ViewModels.Articles;

namespace Negosud.Views
{
    public partial class ArticlesView : UserControl
    {
        public ArticlesView()
        {
            InitializeComponent();
            DataContext = new ArticlesViewModel();

            SetActiveButton(ArticlesButton);
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
            ArticlesButton.Background = new SolidColorBrush(Colors.Transparent);
            ArticlesButton.Effect = null;
            FamiliesButton.Background = new SolidColorBrush(Colors.Transparent);
            FamiliesButton.Effect = null;
        }
    }
}
