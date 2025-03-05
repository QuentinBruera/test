using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Negosud.Converters
{
    public class MultiVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2) return Visibility.Collapsed;

            if (values[0] is bool isAdminMode && !isAdminMode) return Visibility.Collapsed;

            if (values[1] is int statusId && parameter is string param && int.TryParse(param, out int targetStatusId))
            {
                return statusId == targetStatusId ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
