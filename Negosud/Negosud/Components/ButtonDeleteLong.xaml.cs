﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Negosud.Components
{
    public partial class ButtonDeleteLong : UserControl
    {
        public ButtonDeleteLong()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CommandProperty =
           DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ButtonDeleteLong));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true) Command.Execute(null);
        }
    }
}
