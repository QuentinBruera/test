using System.Windows.Media;
using System.Windows;

namespace Negosud.Utils
{
    public static class BadgeColorMapper
    {
        public static Brush GetColorForStatus(string status)
        {
            return status switch
            {
                "Pending" => (Brush)Application.Current.Resources["LossColor"],
                "Done" => (Brush)Application.Current.Resources["ValidateColor"],
                "Cancelled" => (Brush)Application.Current.Resources["CancelColor"],
                _ => (Brush)Application.Current.Resources["BoxColor"] 
            };
        }

        public static Brush GetColorForReason(string reason)
        {
            return reason switch
            {
                "Purchase" => (Brush)Application.Current.Resources["ValidateColor"],
                "Sale" => (Brush)Application.Current.Resources["SaleColor"],
                "Broken" => (Brush)Application.Current.Resources["CancelColor"],
                "Lost" => (Brush)Application.Current.Resources["LossColor"],
                "Tasting" => (Brush)Application.Current.Resources["TastingColor"],
                "Inventory" => (Brush)Application.Current.Resources["InventoryColor"],
                "Correction" => (Brush)Application.Current.Resources["CorrectionColor"],
                _ => (Brush)Application.Current.Resources["BoxColor"] 
            };
        }
    }
}
