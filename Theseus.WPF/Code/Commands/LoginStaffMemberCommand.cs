using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    public class LoginStaffMemberCommand : AsyncCommandBase
    {
        private readonly StaffMemberLoginViewModel _staffMemberLoginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;

        public LoginStaffMemberCommand(StaffMemberLoginViewModel staffMemberLoginViewModel,
                                       IAuthenticator authenticator,
                                       NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            _staffMemberLoginViewModel = staffMemberLoginViewModel;
            _authenticator = authenticator;
            _loggedInNavigationService = loggedInNavigationService;

            this._staffMemberLoginViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.LoginStaffMember(_staffMemberLoginViewModel.Username,
                                                      _staffMemberLoginViewModel.Password);

                this._loggedInNavigationService.Navigate();
            }
            catch (UserNotFoundException)
            {
                //_loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                //_loginViewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                //_loginViewModel.ErrorMessage = "Login failed.";
            }
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_staffMemberLoginViewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _staffMemberLoginViewModel.CanLogin && base.CanExecute(parameter);
        }
    }
}
