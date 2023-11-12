using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.NavigationCommands
{
    public class OpenAccountViewModelCommand : CommandBase
    {
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;
        private readonly NavigationService<NotLoggedInViewModel> _notLoggedInNavigationService;
        private readonly ICurrentPatientStore _currentPatientStore;
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;

        public OpenAccountViewModelCommand(NavigationService<LoggedInViewModel> loggedInNavigationService,
                                           NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                           ICurrentPatientStore currentPatientStore,
                                           ICurrentStaffMemberStore currentStaffMemberStore)
        {
            _loggedInNavigationService = loggedInNavigationService;
            _notLoggedInNavigationService = notLoggedInNavigationService;
            _currentPatientStore = currentPatientStore;
            _currentStaffMemberStore = currentStaffMemberStore;
        }

        public override void Execute(object? parameter)
        {
            if (_currentPatientStore.IsPatientLoggedIn || _currentStaffMemberStore.IsStaffMemberLoggedIn)
            {
                _loggedInNavigationService.Navigate();
            }
            else
            {
                _notLoggedInNavigationService.Navigate();
            }
        }
    }
}
