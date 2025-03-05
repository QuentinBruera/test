using System;
using System.Globalization;
using System.Windows.Data;
using NegosudModel.Dto;

namespace Negosud.Converters
{
    public class AddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CustomerDto customer)
            {
                return $"{customer.Address} {customer.ZipCode} {customer.City}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

