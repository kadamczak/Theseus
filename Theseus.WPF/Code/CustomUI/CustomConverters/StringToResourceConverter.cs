using System;
using System.Windows.Data;
using Theseus.WPF.Code.Extensions;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    /// <summary>
    /// The <c>StringToResourceConverter</c> class converts string key into a string taken from the loaded resource file.
    /// </summary>
    public class StringToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => (value is null) ? "Undisclosed".Resource() : value.ToString().Resource();

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}