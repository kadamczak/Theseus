using System.Windows.Input;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.ViewModels.AccountViewModels.Interfaces;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberDetailsNotLoggedInViewModel : AccountDetailsViewModel
    {
        public ICommand NavigateToStaffMemberLoginRegister { get; }

        public StaffMemberDetailsNotLoggedInViewModel(NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            NavigateToStaffMemberLoginRegister = new NavigateCommand<StaffMemberLoginRegisterViewModel>(staffMemberLoginRegisterNavigationService);
        }
    }
}
