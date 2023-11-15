using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    public class LogoutPatientCommand : CommandBase
    {
        private readonly IPatientAuthenticator _authenticator;
        private readonly NavigationService<PatientLoginRegisterViewModel> _patientLoginRegisterNavigationService;

        public LogoutPatientCommand(IPatientAuthenticator authenticator, NavigationService<PatientLoginRegisterViewModel> patientLoginRegisterNavigationService)
        {
            _authenticator = authenticator;
            _patientLoginRegisterNavigationService = patientLoginRegisterNavigationService;
        }

        public override void Execute(object? parameter)
        {
            RemoveUserFromAutomaticLogIn();

            _authenticator.Logout();
            _patientLoginRegisterNavigationService.Navigate();
        }

        private void RemoveUserFromAutomaticLogIn()
        {
            Properties.Settings.Default.LogInUsername = string.Empty;
            Properties.Settings.Default.Save();
        }
    }
}