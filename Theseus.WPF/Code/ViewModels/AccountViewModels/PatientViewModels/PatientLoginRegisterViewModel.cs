using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class PatientLoginRegisterViewModel : ViewModelBase
    {
        public StaffMemberLoginViewModel StaffMemberLoginViewModel { get; }
        public StaffMemberRegisterViewModel StaffMemberRegisterViewModel { get; }

        public ICommand GoBack { get; }

        public PatientLoginRegisterViewModel(StaffMemberLoginViewModel staffMemberLoginViewModel,
                                             StaffMemberRegisterViewModel staffMemberRegisterViewModel,
                                             NavigationService<LoggedInViewModel> loggedInNavigationService,
                                             NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                             ICurrentUserStore currentUserStore)
        {
            StaffMemberLoginViewModel = staffMemberLoginViewModel;
            StaffMemberRegisterViewModel = staffMemberRegisterViewModel;
            this.GoBack = new OpenAccountViewModelCommand(loggedInNavigationService, notLoggedInNavigationService, currentUserStore);
        }
    }
}