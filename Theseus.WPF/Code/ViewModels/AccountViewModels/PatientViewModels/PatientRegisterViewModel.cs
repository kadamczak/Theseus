using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.AccountCommands.PatientCommands;
using Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientRegisterViewModel : ViewModelBase
    {
        private string _patientUsername = string.Empty;

        public string PatientUsername
        {
            get => _patientUsername;
            set
            {
                _patientUsername = value;
                OnPropertyChanged(nameof(PatientUsername));
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
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public bool CanRegister => !string.IsNullOrEmpty(PatientUsername) && !string.IsNullOrEmpty(StaffMemberUsername);

        public ICommand Register { get; }

        public PatientRegisterViewModel(IPatientAuthenticator authenticator)
        {
            Register = new RegisterPatientCommand(this, authenticator);
        }
    }
}