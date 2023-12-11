using System;
using System.Windows.Data;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    public class StringToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is null)
                return (string)App.Current.Resources["Undisclosed"];

            return (string)App.Current.Resources[value.ToString()];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}