using System;
using System.Threading.Tasks;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    /// <summary>
    /// The <c>AttemptToLogInAutomaticallyCommand</c> is called upon application start-up in order to automatically log-in the last <c>Patient</c>, if he/she
    /// has not logged out.
    /// </summary>
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
            string loggedInGroup = Properties.Settings.Default.LogInGroup;

            if (string.IsNullOrWhiteSpace(loggedInPatientUsername) || string.IsNullOrWhiteSpace(loggedInGroup))
            {
                _notLoggedInNavigationService.Navigate();
            }
            else
            {
                try
                {
                    await _authenticator.Login(loggedInPatientUsername, loggedInGroup);
                }
                catch (Exception)
                {
                    ResetStoredLogInData();
                    _notLoggedInNavigationService.Navigate();
                    return;
                }

                _loggedInNavigationService.Navigate();
            }
        }

        private void ResetStoredLogInData()
        {
            Properties.Settings.Default.LogInUsername = "";
            Properties.Settings.Default.LogInGroup = "";
            Properties.Settings.Default.Save();
        }
    }
}