using System;
using System.Globalization;
using System.Windows.Data;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    /// <summary>
    /// The <c>ComparisonConverter</c> class aids in converting RadioButton etc. choices into values usable by view models.
    /// </summary>
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.Equals(parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.Equals(true) == true ? parameter : Binding.DoNothing;
    }
}