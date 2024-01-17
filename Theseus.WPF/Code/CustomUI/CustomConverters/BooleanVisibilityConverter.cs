using System.Windows;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    /// <summary>
    /// The <c>BooleanVisibilityConverter</c> class converts bool values to Visibility enum values (True -> Visibility.Visible and False -> Visibility.Collapsed by default).
    /// </summary>
    public class BooleanVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}