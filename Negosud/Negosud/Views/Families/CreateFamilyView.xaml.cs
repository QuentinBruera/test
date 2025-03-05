using Negosud.ViewModels.Families;
using System.Windows.Controls;

namespace Negosud.Views
{
    public partial class CreateFamilyView : UserControl
    {
        public CreateFamilyView()
        {
            InitializeComponent();
            this.DataContext = new CreateFamilyViewModel();
        }
    }
}
