using System.Windows;
using System.Windows.Controls;

namespace Negosud.Components
{
    public partial class CustomInput : UserControl
    {
        public CustomInput()
        {
            InitializeComponent();
        }

        // Propriété pour le titre
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(CustomInput), new PropertyMetadata(string.Empty));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        // Propriété pour l'input
        public static readonly DependencyProperty InputValueProperty = DependencyProperty.Register(nameof(InputValue), typeof(string), typeof(CustomInput), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string InputValue
        {
            get => (string)GetValue(InputValueProperty);
            set => SetValue(InputValueProperty, value);
        }
    }
}
