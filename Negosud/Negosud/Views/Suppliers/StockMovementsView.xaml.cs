using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows;
using NegosudModel.Dto;
using Negosud.ViewModels.Stock;

namespace Negosud.Views
{
    public partial class StockMovementsView : Window
    {
        public ObservableCollection<StockMovementViewModel> StockMovements { get; }

        public StockMovementsView(ObservableCollection<StockMovementViewModel> stockMovements)
        {
            InitializeComponent();

            // Assigner les mouvements de stock
            StockMovements = stockMovements;

            // Définir le DataContext pour le binding
            DataContext = this;
        }
    }
}
