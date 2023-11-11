using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class NotLoggedInViewModel : ViewModelBase
    {
        public ICommand NavigateToStaffMemberLoginRegister { get; }

        public NotLoggedInViewModel(NavigationService<StaffMemberLoginRegisterViewModel> navigateToStaffMemberLoginRegister)
        {
            NavigateToStaffMemberLoginRegister = new NavigateCommand<StaffMemberLoginRegisterViewModel>(navigateToStaffMemberLoginRegister);
        }


    }
}