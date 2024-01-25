using System;
using System.Globalization;
using System.Windows.Data;
using Theseus.WPF.Code.Extensions;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    /// <summary>
    /// The <c>NullableIntConverter</c> converts null integers into either a string representation of the value
    /// or displays "Undisclosed" message if the int is null.
    /// </summary>
    public class NullableIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (value is null) ? "Undisclosed".Resource() : value.ToString();        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}