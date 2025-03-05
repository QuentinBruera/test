using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Negosud.Components
{
    public partial class ButtonValidate : UserControl
    {
        public ButtonValidate()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ButtonValidate), new PropertyMetadata(null));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ButtonValidate), new PropertyMetadata(null));
    }
}
