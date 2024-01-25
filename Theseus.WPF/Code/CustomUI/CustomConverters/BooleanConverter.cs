using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    /// <summary>
    /// The <c>BooleanConverter</c> converts bool values to values of another type.
    /// </summary>
    /// <typeparam name="T">Output type.</typeparam>
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)      
            => value is bool && ((bool)value) ? True : False;

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is T && EqualityComparer<T>.Default.Equals((T)value, True);
    }
}
