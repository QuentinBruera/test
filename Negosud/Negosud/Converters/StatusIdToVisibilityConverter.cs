using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Negosud.Converters
{
    public class StatusIdToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int statusId && parameter is string statusParameter)
            {
                if (int.TryParse(statusParameter, out int targetStatus)) return statusId == targetStatus ? Visibility.Visible : Visibility.Collapsed;
                
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
