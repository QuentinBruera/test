
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Negosud.Components
{
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();
        }

        private void OnNavButtonClick(object sender, RoutedEventArgs e)
        {
            ResetButtonColors();

            // Au clic, change la couleur du bouton en rouge
            Button button = (Button)sender;
            button.Background = (Brush)Application.Current.Resources["MainBackgroundColor"];
            button.Foreground = (Brush)Application.Current.Resources["SecondaryItemColor"];

            // Appliquer l'ombre
            DropShadowEffect shadowEffect = new DropShadowEffect
            {
                ShadowDepth = 2,
                BlurRadius = 4,
                Color = Colors.Black,
                Opacity = 0.25
            };
            button.Effect = shadowEffect;

            // Récupérer le StackPanel enfant du bouton
            StackPanel? stackPanel = button.Content as StackPanel;
            if (stackPanel != null)
            {
                // Récupérer le Rectangle enfant du StackPanel
                Rectangle? rectangle = stackPanel.Children.OfType<Rectangle>().FirstOrDefault();
                if (rectangle != null)
                {
                    rectangle.Fill = (Brush)Application.Current.Resources["UnderlineColor"];
                }
            }
        }

        private void ResetButtonColors()
        {
            // Parcourt tous les enfants du StackPanel
            foreach (var child in NavStackPanel.Children)
            {
                if (child is Button button) // Vérifie si c'est un bouton
                {
                    button.Background = new SolidColorBrush(Colors.Transparent);
                    button.Foreground = new SolidColorBrush(Colors.Black);
                    button.Effect = null;

                    // Récupérer le StackPanel enfant du bouton
                    StackPanel? stackPanel = button.Content as StackPanel;
                    if (stackPanel != null)
                    {
                        // Récupérer le Rectangle enfant du StackPanel
                        Rectangle? rectangle = stackPanel.Children.OfType<Rectangle>().FirstOrDefault();
                        if (rectangle != null)
                        {
                            rectangle.Fill = new SolidColorBrush(Colors.Transparent);
                            rectangle.Effect = null;
                        }
                    }
                }
            }
        }
    }
}
