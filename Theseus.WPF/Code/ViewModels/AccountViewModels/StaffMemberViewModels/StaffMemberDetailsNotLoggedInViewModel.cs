using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class StaffMemberDetailsNotLoggedInViewModel : ViewModelBase
    {
        public ICommand NavigateToStaffMemberLoginRegister { get; }

        public StaffMemberDetailsNotLoggedInViewModel(NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            NavigateToStaffMemberLoginRegister = new NavigateCommand<StaffMemberLoginRegisterViewModel>(staffMemberLoginRegisterNavigationService);
        }
    }
}
