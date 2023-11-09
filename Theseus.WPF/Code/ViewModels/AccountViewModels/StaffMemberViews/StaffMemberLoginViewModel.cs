using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberLoginViewModel : ViewModelBase
    {
        private string _username = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(CanLogin));
            }
        }


        public bool CanLogin => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public ICommand Login { get; }

        public StaffMemberLoginViewModel(IAuthenticator authenticator, NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            Login = new LoginStaffMemberCommand(this, authenticator, loggedInNavigationService);
        }
    }
}