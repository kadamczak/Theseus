using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberLoginViewModel : ErrorCheckingViewModel
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

                if (!Regex.IsMatch(_username, @"^[\w_]*$"))
                {
                    AddError(nameof(Username), "UsernameContainsInvalidCharacters".Resource());
                }

                OnPropertyChanged(nameof(CanLogin));
            }
        }

        private string _password = string.Empty;
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
                ClearErrors(nameof(Password));
                OnPropertyChanged(nameof(CanLogin));
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

        public bool CanLogin => !HasErrors && !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

        public ICommand Login { get; }

        public StaffMemberLoginViewModel(IStaffMemberAuthenticator authenticator, NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            Login = new LoginStaffMemberCommand(this, authenticator, loggedInNavigationService);
        }
    }
}