using System;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientLoginViewModel : ErrorCheckingViewModel
    {
        private string _username = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));

                ClearErrors(nameof(Username));

                if (string.IsNullOrWhiteSpace(Username))
                {
                    AddError(nameof(Username), "Field can't be empty.");
                }

                OnPropertyChanged(nameof(CanLogin));
            }
        }

        public string _pastUsernameFirst = string.Empty;
        public string PastUsernameFirst
        {
            get => _pastUsernameFirst;
            set
            {
                _pastUsernameFirst = value;
                OnPropertyChanged(nameof(PastUsernameFirst));
            }
        }

        public string _pastUsernameSecond = string.Empty;
        public string PastUsernameSecond
        {
            get => _pastUsernameSecond;
            set
            {
                _pastUsernameSecond = value;
                OnPropertyChanged(nameof(PastUsernameSecond));
            }
        }

        private string _loginResponse = string.Empty;
        public string LoginResponse
        {
            get => _loginResponse;
            set
            {
                _loginResponse = value;
                OnPropertyChanged(nameof(LoginResponse));
            }
        }

        public bool CanLogin => !HasErrors;
        public ICommand Login { get; }

        public PatientLoginViewModel(IPatientAuthenticator authenticator, NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            ClearFields();
            LoadPastUsernames();

            Login = new LoginPatientCommand(this, authenticator, loggedInNavigationService);
        }

        private void ClearFields()
        {
            Username = string.Empty;
        }

        private void LoadPastUsernames()
        {
            PastUsernameFirst = Properties.Settings.Default.PastUsernameFirst;
            PastUsernameSecond = Properties.Settings.Default.PastUsernameSecond;
        }
    }
}