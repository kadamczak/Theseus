using System.Threading.Tasks;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    public class AttemptToLogInAutomaticallyCommand : AsyncCommandBase
    {
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;
        private readonly NavigationService<NotLoggedInViewModel> _notLoggedInNavigationService;

        private readonly IPatientAuthenticator _authenticator;

        public AttemptToLogInAutomaticallyCommand(NavigationService<LoggedInViewModel> loggedInNavigationService,
                                                  NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                                  IPatientAuthenticator authenticator)
        {
            _loggedInNavigationService = loggedInNavigationService;
            _notLoggedInNavigationService = notLoggedInNavigationService;
            _authenticator = authenticator;
        }

        public override async Task ExecuteAsync(object? parameter = null)
        {
            string loggedInPatientUsername = Properties.Settings.Default.LogInUsername;

            if(string.IsNullOrWhiteSpace(loggedInPatientUsername))
            {
                _notLoggedInNavigationService.Navigate();
            }
            else
            {
                await _authenticator.Login(loggedInPatientUsername);
                _loggedInNavigationService.Navigate();
            }
        }
    }
}