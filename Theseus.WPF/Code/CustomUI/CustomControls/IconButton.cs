using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Theseus.WPF.Code.CustomUI.CustomControls
{
    /// <summary>
    /// The <c>IconButton</c> class defines property fields of the IconButton control.
    /// </summary>
    public class IconButton : ButtonBase
    {
        static IconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(IconButton), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
    }
}
