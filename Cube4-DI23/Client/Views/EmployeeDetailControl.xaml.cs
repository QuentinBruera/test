using Client.ViewModels;
using System.Windows.Controls;

namespace Client.Views
{
    /// <summary>
    /// Logique d'interaction pour EmployeeDetailControl.xaml
    /// </summary>
    public partial class EmployeeDetailControl : UserControl
    {
        public EmployeeDetailControl(int employeeId)
        {
            InitializeComponent();
            DataContext = new EmployeeDetailViewModel(employeeId);
        }
    }
}