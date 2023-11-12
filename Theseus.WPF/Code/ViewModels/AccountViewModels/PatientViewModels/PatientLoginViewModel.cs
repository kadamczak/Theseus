using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientLoginViewModel : ViewModelBase
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

        public bool CanLogin => !string.IsNullOrEmpty(Username);
        public ICommand Login { get; }

        public PatientLoginViewModel(IPatientAuthenticator authenticator, NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            Login = new LoginPatientCommand(this, authenticator, loggedInNavigationService);
        }
    }
}