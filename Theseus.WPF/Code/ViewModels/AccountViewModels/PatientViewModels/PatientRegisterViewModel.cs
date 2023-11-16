using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
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

                if (string.IsNullOrWhiteSpace(PatientUsername))
                {
                    AddError(nameof(PatientUsername), "Field can't be empty.");
                }

                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _staffMemberUsername = string.Empty;

        public string StaffMemberUsername
        {
            get => _staffMemberUsername;
            set
            {
                _staffMemberUsername = value;
                OnPropertyChanged(nameof(StaffMemberUsername));

                ClearErrors(nameof(StaffMemberUsername));

                if (string.IsNullOrWhiteSpace(StaffMemberUsername))
                {
                    AddError(nameof(StaffMemberUsername), "Field can't be empty.");
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

        public bool CanRegister => !HasErrors;

        public ICommand Register { get; }

        public PatientRegisterViewModel(IPatientAuthenticator authenticator)
        {
            ClearFields();
            Register = new RegisterPatientCommand(this, authenticator);
        }

        private void ClearFields()
        {
            PatientUsername = string.Empty;
            StaffMemberUsername = string.Empty;
        }
    }
}