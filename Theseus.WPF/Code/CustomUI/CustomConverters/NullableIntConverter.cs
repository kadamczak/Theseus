using System;
using System.Globalization;
using System.Windows.Data;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    public class NullableIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return (string)App.Current.Resources["Undisclosed"];

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}