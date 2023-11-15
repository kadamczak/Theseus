using System.Windows;
using System.Windows.Controls;

namespace Theseus.WPF.Code.CustomUI.CustomControls
{
    public class FormField : TextBox
    {
        static FormField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormField), new FrameworkPropertyMetadata(typeof(FormField)));
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(FormField), new PropertyMetadata(null));

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text), typeof(string), typeof(FormField), new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}