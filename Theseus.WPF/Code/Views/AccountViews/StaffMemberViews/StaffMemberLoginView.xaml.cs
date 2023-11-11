using System.Windows;
using System.Windows.Controls;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for StaffMemberLoginView.xaml
    /// </summary>
    public partial class StaffMemberLoginView : UserControl
    {
        public StaffMemberLoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }


    }
}
