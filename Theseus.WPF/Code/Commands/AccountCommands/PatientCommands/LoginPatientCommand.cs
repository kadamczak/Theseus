using System;
using System.ComponentModel;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    public class LoginPatientCommand : CommandBase
    {
        private readonly PatientLoginViewModel _patientLoginViewModel;
        private readonly IPatientAuthenticator _authenticator;
        private readonly NavigationService<LoggedInViewModel> _loggedInNavigationService;

        public LoginPatientCommand(PatientLoginViewModel patientLoginViewModel,
                                   IPatientAuthenticator authenticator,
                                   NavigationService<LoggedInViewModel> loggedInNavigationService)
        {
            _patientLoginViewModel = patientLoginViewModel;
            _authenticator = authenticator;
            _loggedInNavigationService = loggedInNavigationService;

            _patientLoginViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async void Execute(object? parameter)
        {
            try
            {
                await _authenticator.Login(_patientLoginViewModel.Username);

                _loggedInNavigationService.Navigate();
                UpdateLastStoredPatientUsernames();
            }
            catch (UserNotFoundException)
            {
                //_loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (Exception)
            {
                //_loginViewModel.ErrorMessage = "Login failed.";
            }
        }

        private void UpdateLastStoredPatientUsernames()
        {
            string currentUsername = _patientLoginViewModel.Username;
            Properties.Settings.Default.LogInUsername = currentUsername;

            if(Properties.Settings.Default.PastUsernameFirst != currentUsername)
            {
                UpdateRecentUsernameList(currentUsername);
            }

            Properties.Settings.Default.Save();
        }

        private void UpdateRecentUsernameList(string username)
        {
            Properties.Settings.Default.PastUsernameSecond = Properties.Settings.Default.PastUsernameFirst;
            Properties.Settings.Default.PastUsernameFirst = username;
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_patientLoginViewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _patientLoginViewModel.CanLogin && base.CanExecute(parameter);
        }
    }
}