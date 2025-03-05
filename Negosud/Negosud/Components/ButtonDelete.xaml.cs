using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Negosud.Components
{
    public partial class ButtonDelete : UserControl
    {
        public ButtonDelete()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DeleteCommandProperty =
           DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ButtonDelete), new PropertyMetadata(null));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(ButtonDelete), new PropertyMetadata(null));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
