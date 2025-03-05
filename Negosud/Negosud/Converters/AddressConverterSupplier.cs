using System;
using System.Globalization;
using System.Windows.Data;
using NegosudModel.Dto;


namespace Negosud.Converters
{
public class AddressConverterSupplier : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SupplierDto supplier)
        {
            return $"{supplier.Address} {supplier.ZipCode} {supplier.City}";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}