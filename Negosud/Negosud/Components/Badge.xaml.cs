using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Negosud.Components
{
    public partial class Badge : UserControl
    {
        public Badge()
        {
            InitializeComponent();
        }

        public static readonly new DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(Badge), new PropertyMetadata(default(string)));

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(Badge), new PropertyMetadata(default(Brush)));

        public new string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
    }
}
