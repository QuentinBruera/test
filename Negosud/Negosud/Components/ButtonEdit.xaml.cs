using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Negosud.Components
{
    public partial class ButtonEdit : UserControl
    {
        public ButtonEdit()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NavigateCommandProperty = DependencyProperty.Register("NavigateCommand", typeof(ICommand), typeof(ButtonEdit), new PropertyMetadata(null));

        public ICommand NavigateCommand
        {
            get => (ICommand)GetValue(NavigateCommandProperty);
            set => SetValue(NavigateCommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ButtonEdit), new PropertyMetadata(null));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
