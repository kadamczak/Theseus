using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Theseus.WPF.Code.CustomUI.CustomControls
{
    /// <summary>
    /// The <c>IconTextButton</c> class defines property fields of the IconTextButton control.
    /// </summary>
    public class IconTextButton : ButtonBase
    {
        static IconTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconTextButton), new FrameworkPropertyMetadata(typeof(IconTextButton)));
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(IconTextButton), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty); 
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(IconTextButton), new PropertyMetadata(null));

        public string Text
        {
            get => (string)GetValue(TextProperty); 
            set => SetValue(TextProperty, value);
        }
    }
}