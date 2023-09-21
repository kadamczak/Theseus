using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Theseus.Code.CustomControls
{
    public class SidebarButton : ButtonBase
    {
        static SidebarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SidebarButton), new FrameworkPropertyMetadata(typeof(SidebarButton)));
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(SidebarButton), new PropertyMetadata(null));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SidebarButton), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        //public static readonly DependencyProperty NavUriProperty = DependencyProperty.Register("NavUri", typeof(Uri), typeof(SidebarButton), new PropertyMetadata(null));

        //public Uri NavUri
        //{
        //    get { return (Uri)GetValue(NavUriProperty); }
        //    set { SetValue(NavUriProperty, value); }
        //}

    }
}
