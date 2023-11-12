using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated;
using Theseus.Domain.Services.Authentication;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    public class RegisterStaffMemberCommand : AsyncCommandBase
    {
        private readonly StaffMemberRegisterViewModel _staffMemberRegisterViewModel;
        private readonly IStaffMemberAuthenticator _authenticator;
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;

        public RegisterStaffMemberCommand(StaffMemberRegisterViewModel staffMemberRegisterViewModel,
                                          IStaffMemberAuthenticator authenticator,
                                          NavigationService<LoggedInViewModel> loggedInViewModel)
        {
            _staffMemberRegisterViewModel = staffMemberRegisterViewModel;
            _authenticator = authenticator;
            _loggedInNavigationService = loggedInViewModel;

            _staffMemberRegisterViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            StaffMember newStaffMember = new StaffMember()
            {
                Id = Guid.NewGuid(),
                Username = _staffMemberRegisterViewModel.Username,
                Email = _staffMemberRegisterViewModel.Email,
                Name = _staffMemberRegisterViewModel.Name,
                Surname = _staffMemberRegisterViewModel.Surname,
                PasswordHash = _staffMemberRegisterViewModel.Password
            };

            RegistrationResult registrationResult = await _authenticator.Register(newStaffMember, _staffMemberRegisterViewModel.ConfirmPassword);
            //TODO
            if (registrationResult == RegistrationResult.Success)
            {
                //this._loggedInNavigationService.Navigate();
            }
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_staffMemberRegisterViewModel.CanRegister))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _staffMemberRegisterViewModel.CanRegister && base.CanExecute(parameter);
        }
    }
}