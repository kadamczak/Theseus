using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    public class LogoutStaffMemberCommand : CommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly NavigationService<StaffMemberLoginRegisterViewModel> _staffMemberLoginRegisterNavigationService;

        public LogoutStaffMemberCommand(IAuthenticator authenticator,
                                        NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            _authenticator = authenticator;
            _staffMemberLoginRegisterNavigationService = staffMemberLoginRegisterNavigationService;
        }

        public override void Execute(object? parameter)
        {
            _authenticator.LogoutStaffMember();

            _staffMemberLoginRegisterNavigationService.Navigate();
        }
    }
}
