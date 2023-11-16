using System;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
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

                if (string.IsNullOrWhiteSpace(Username))
                {
                    AddError(nameof(Username), "Field can't be empty.");
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

                if (string.IsNullOrWhiteSpace(Password))
                {
                    AddError(nameof(Password), "Field can't be empty.");
                }

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

        public bool CanLogin => !HasErrors;

        public ICommand Login { get; }

        public StaffMemberLoginViewModel(IStaffMemberAuthenticator authenticator, NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            ClearFields();
            Login = new LoginStaffMemberCommand(this, authenticator, loggedInNavigationService);
        }

        private void ClearFields()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}