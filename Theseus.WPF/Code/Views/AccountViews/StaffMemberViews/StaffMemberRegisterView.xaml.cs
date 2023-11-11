using System.Windows.Controls;

namespace Theseus.WPF.Code.Views
{
    /// <summary>
    /// Interaction logic for StaffMemberRegisterView.xaml
    /// </summary>
    public partial class StaffMemberRegisterView : UserControl
    {
        public StaffMemberRegisterView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }

    }
}
