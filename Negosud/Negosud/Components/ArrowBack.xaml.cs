using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Negosud.Components
{
    public partial class ArrowBack : UserControl
    {
        public ArrowBack()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NavigateBackCommandProperty = DependencyProperty.Register("NavigateBackCommand", typeof(ICommand), typeof(ArrowBack), new PropertyMetadata(null));

        public ICommand NavigateBackCommand
        {
            get => (ICommand)GetValue(NavigateBackCommandProperty);
            set => SetValue(NavigateBackCommandProperty, value);
        }
    }
}
