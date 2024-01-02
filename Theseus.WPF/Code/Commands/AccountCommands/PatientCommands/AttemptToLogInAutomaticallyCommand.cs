using System;
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
            //Properties.Settings.Default.LogInUsername = "";
            //Properties.Settings.Default.LogInGroup = "";
            //Properties.Settings.Default.PastGroupFirst = "";
            //Properties.Settings.Default.PastGroupSecond = "";
            //Properties.Settings.Default.PastUsernameFirst = "";
            //Properties.Settings.Default.PastUsernameSecond = "";
            //Properties.Settings.Default.Save();

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