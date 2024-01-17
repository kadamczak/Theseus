
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c> StaffMemberRegisterViewModel</c> class contains bindings for StaffMember Register View.
    /// </summary>
    public class StaffMemberRegisterViewModel : ErrorCheckingViewModel
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

                if (!Regex.IsMatch(Username, @"^[\w_]*$"))
                {
                    AddError(nameof(Username), "UsernameContainsInvalidCharacters".Resource());
                }

                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                ClearErrors(nameof(Email));

                if (!_emailValidator.IsValid(Email))
                {
                    AddError(nameof(Email), "EmailInvalid".Resource());
                }

                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _surname = string.Empty;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
                OnPropertyChanged(nameof(CanRegister));
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

                if (Password.Length < 6)
                {
                    AddError(nameof(Password), "Password too short.");
                }

                if (!Password.Any(c => char.IsDigit(c)))
                {
                    AddError(nameof(Password), "Password has no numbers.");
                }

                if (!Password.Any(c => char.IsLower(c)) || !Password.Any(c => char.IsUpper(c)))
                {
                    AddError(nameof(Password), "Password doesn't have both lower and upper case letters.");
                }

                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _registrationResponse = string.Empty;
        public string RegistrationResponse
        {
            get => _registrationResponse;
            set
            {
                _registrationResponse = value;
                OnPropertyChanged(nameof(RegistrationResponse));
            }
        }

        private readonly EmailValidator _emailValidator;

        public bool CanRegister => !HasErrors &&
                                   !string.IsNullOrWhiteSpace(Username) &&
                                   !string.IsNullOrWhiteSpace(Name) &&
                                   !string.IsNullOrWhiteSpace(Surname) &&
                                   !string.IsNullOrWhiteSpace(Email) &&
                                   !string.IsNullOrWhiteSpace(Password) &&
                                   !string.IsNullOrWhiteSpace(ConfirmPassword);

        public ICommand Register { get; }

        public StaffMemberRegisterViewModel(IStaffMemberAuthenticator authenticator, EmailValidator emailValidator)
        {
            this._emailValidator = emailValidator;
            Register = new RegisterStaffMemberCommand(this, authenticator);
        }
    }
}