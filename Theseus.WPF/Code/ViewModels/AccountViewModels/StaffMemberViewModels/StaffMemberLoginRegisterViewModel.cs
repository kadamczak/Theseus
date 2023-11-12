using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberLoginRegisterViewModel : ViewModelBase
    {
        public StaffMemberLoginViewModel StaffMemberLoginViewModel { get; }
        public StaffMemberRegisterViewModel StaffMemberRegisterViewModel { get; }

        public ICommand GoBack { get; }

        public StaffMemberLoginRegisterViewModel(StaffMemberLoginViewModel staffMemberLoginViewModel,
                                                 StaffMemberRegisterViewModel staffMemberRegisterViewModel,
                                                 NavigationService<LoggedInViewModel> loggedInNavigationService,
                                                 NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                                 ICurrentPatientStore currentPatientStore,
                                                 ICurrentStaffMemberStore currentStaffMemberStore)
        {
            StaffMemberLoginViewModel = staffMemberLoginViewModel;
            StaffMemberRegisterViewModel = staffMemberRegisterViewModel;

            this.GoBack = new OpenAccountViewModelCommand(loggedInNavigationService,
                                                          notLoggedInNavigationService,
                                                          currentPatientStore,
                                                          currentStaffMemberStore);
        }
    }
}