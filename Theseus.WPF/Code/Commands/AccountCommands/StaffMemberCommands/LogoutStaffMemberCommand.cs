using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    public class LogoutStaffMemberCommand : CommandBase
    {
        private readonly IStaffMemberAuthenticator _authenticator;
        private readonly NavigationService<StaffMemberLoginRegisterViewModel> _staffMemberLoginRegisterNavigationService;

        public LogoutStaffMemberCommand(IStaffMemberAuthenticator authenticator,
                                        NavigationService<StaffMemberLoginRegisterViewModel> staffMemberLoginRegisterNavigationService)
        {
            _authenticator = authenticator;
            _staffMemberLoginRegisterNavigationService = staffMemberLoginRegisterNavigationService;
        }

        public override void Execute(object? parameter)
        {
            _authenticator.Logout();
            _staffMemberLoginRegisterNavigationService.Navigate();
        }
    }
}