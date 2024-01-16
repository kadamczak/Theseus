using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    /// <summary>
    /// The <c>RegisterPatientCommand</c> attempts to register a new <c>Patient</c> account using data from <c>PatientRegisterViewModel</c>.
    /// </summary>
    public class RegisterPatientCommand : AsyncCommandBase
    {
        private readonly PatientRegisterViewModel _patientRegisterViewModel;
        private readonly IPatientAuthenticator _authenticator;

        public RegisterPatientCommand(PatientRegisterViewModel patientRegisterViewModel, IPatientAuthenticator authenticator)
        {
            _patientRegisterViewModel = patientRegisterViewModel;
            _authenticator = authenticator;

            _patientRegisterViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Patient newPatient = new Patient()
            {
                Id = Guid.NewGuid(),
                Username = _patientRegisterViewModel.PatientUsername.Trim()
            };

            string staffMemberUsername = _patientRegisterViewModel.GroupName;

            PatientRegistrationResult registrationResult = await _authenticator.Register(newPatient, staffMemberUsername);
            this._patientRegisterViewModel.RegistrationResponse = GetRegistrationResponse(registrationResult);
        }

        private string GetRegistrationResponse(PatientRegistrationResult registrationResult)
        {
            return registrationResult switch
            {
                PatientRegistrationResult.Success => "SuccesfullyRegistered".Resource(),
                PatientRegistrationResult.UsernameAlreadyExists => "PatientUsernameAlreadyExists".Resource(),
                PatientRegistrationResult.GroupDoesNotExist => "GroupDoesNotExist".Resource(),
                PatientRegistrationResult.ConnectionFailed => "RegistrationFailed".Resource(),
                PatientRegistrationResult.PatientDataNotValid => "DataNotValid".Resource(),
                _ => throw new ArgumentException("Invalid parameter.")
            };
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_patientRegisterViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _patientRegisterViewModel.CanRegister && base.CanExecute(parameter);
        }
    }
}