using System.Windows;

namespace Theseus.WPF.Code.CustomUI.CustomConverters
{
    public class BooleanVisibilityConverter : BooleanConverter<Visibility>
    {
        public BooleanVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
}