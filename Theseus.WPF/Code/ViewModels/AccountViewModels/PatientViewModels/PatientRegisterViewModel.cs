using System.Text.RegularExpressions;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientRegisterViewModel : ErrorCheckingViewModel
    {
        private string _patientUsername = string.Empty;

        public string PatientUsername
        {
            get => _patientUsername;
            set
            {
                _patientUsername = value;
                OnPropertyChanged(nameof(PatientUsername));
                ClearErrors(nameof(PatientUsername));

                if (!Regex.IsMatch(_patientUsername, @"^[\w_]*$"))
                {
                    AddError(nameof(PatientUsername), "UsernameContainsInvalidCharacters".Resource());
                }

                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _groupName = string.Empty;

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
                ClearErrors(nameof(GroupName));

                if (!Regex.IsMatch(_groupName, @"^[\w-_]*$"))
                {
                    AddError(nameof(GroupName), "NameContainsInvalidCharacters".Resource());
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

        public bool CanRegister => !HasErrors &&
                                   !string.IsNullOrWhiteSpace(PatientUsername) &&
                                   !string.IsNullOrWhiteSpace(GroupName);

        public ICommand Register { get; }

        public PatientRegisterViewModel(IPatientAuthenticator authenticator)
        {
            Register = new RegisterPatientCommand(this, authenticator);
        }
    }
}