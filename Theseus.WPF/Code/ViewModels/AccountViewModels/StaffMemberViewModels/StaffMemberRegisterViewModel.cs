
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
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

                if (string.IsNullOrWhiteSpace(Username))
                {
                    AddError(nameof(Username), "Field can't be empty.");
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
                    AddError(nameof(Email), "Value invalid.");
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

                ClearErrors(nameof(Name));

                if (string.IsNullOrWhiteSpace(Name))
                {
                    AddError(nameof(Name), "Field can't be empty.");
                }

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

                ClearErrors(nameof(Surname));

                if (string.IsNullOrWhiteSpace(Surname))
                {
                    AddError(nameof(Surname), "Field can't be empty.");
                }

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

                if (string.IsNullOrWhiteSpace(Password))
                {
                    AddError(nameof(Password), "Field can't be empty.");
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

                ClearErrors(nameof(ConfirmPassword));

                if (string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    AddError(nameof(ConfirmPassword), "Field can't be empty.");
                }

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

        private readonly IEmailValidator _emailValidator;

        public bool CanRegister => !HasErrors;

        public ICommand Register { get; }

        public StaffMemberRegisterViewModel(IStaffMemberAuthenticator authenticator, IEmailValidator emailValidator)
        {
            this._emailValidator = emailValidator;
            ClearFields();
            Register = new RegisterStaffMemberCommand(this, authenticator);
        }

        private void ClearFields()
        {
            Username = string.Empty;
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}