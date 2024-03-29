﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.StaffMemberCommands
{
    /// <summary>
    /// The <c>LoginStaffMemberCommand </c> class uses data from the <c>StaffMemberLoginViewModel</c> to login a <c>StaffMember</c>
    /// through <c>IStaffMemberAuthenticator</c>.
    /// </summary>
    public class LoginStaffMemberCommand : AsyncCommandBase
    {
        private readonly StaffMemberLoginViewModel _staffMemberLoginViewModel;
        private readonly IStaffMemberAuthenticator _authenticator;
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;

        public LoginStaffMemberCommand(StaffMemberLoginViewModel staffMemberLoginViewModel,
                                       IStaffMemberAuthenticator authenticator,
                                       NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            _staffMemberLoginViewModel = staffMemberLoginViewModel;
            _authenticator = authenticator;
            _loggedInNavigationService = loggedInNavigationService;

            _staffMemberLoginViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _authenticator.Login(_staffMemberLoginViewModel.Username,
                                           _staffMemberLoginViewModel.Password);

                _loggedInNavigationService.Navigate();
            }
            catch (UserNotFoundException)
            {
                _staffMemberLoginViewModel.LoginResponse = "UsernameDoesNotExist".Resource();
            }
            catch (InvalidPasswordException)
            {
                _staffMemberLoginViewModel.LoginResponse = "IncorrectPassword".Resource();
            }
            catch (Exception)
            {
                _staffMemberLoginViewModel.LoginResponse = "LoginFailed".Resource();
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
