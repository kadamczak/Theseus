using System;
using System.ComponentModel;
using Theseus.Domain.Models.GroupRelated.Exceptions;
using Theseus.Domain.Models.UserRelated.Exceptions;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Extensions;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands.AccountCommands.PatientCommands
{
    public class LoginPatientCommand : CommandBase
    {
        private readonly PatientLoginViewModel _patientLoginViewModel;
        private readonly IPatientAuthenticator _authenticator;
        private readonly NavigationService<BeginTestViewModel> _beginTestNavigationService;

        public LoginPatientCommand(PatientLoginViewModel patientLoginViewModel,
                                   IPatientAuthenticator authenticator,
                                   NavigationService<BeginTestViewModel> beginTestNavigationService)
        {
            _patientLoginViewModel = patientLoginViewModel;
            _authenticator = authenticator;
            _beginTestNavigationService = beginTestNavigationService;

            _patientLoginViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public override async void Execute(object? parameter)
        {
            try
            {
                await _authenticator.Login(_patientLoginViewModel.Username, _patientLoginViewModel.GroupName);

                _beginTestNavigationService.Navigate();
                UpdateLastStoredPatientData();
            }
            catch (UserNotFoundException)
            {
                _patientLoginViewModel.LoginResponse = "UsernameDoesNotExist".Resource();
            }
            catch (WrongGroupNameForPatientException)
            {
                _patientLoginViewModel.LoginResponse = "IncorrectGroupName".Resource();
            }
            catch (Exception)
            {
                _patientLoginViewModel.LoginResponse = "LoginFailed".Resource();
            }
        }

        private void UpdateLastStoredPatientData()
        {
            string currentUsername = _patientLoginViewModel.Username;
            string currentGroup = _patientLoginViewModel.GroupName;
            UpdateLogInList(currentUsername, currentGroup);

            if(Properties.Settings.Default.PastUsernameFirst != currentUsername)
            {
                UpdateRecentUsernameList(currentUsername);
                UpdateRecentGroupList(currentGroup);
            }

            Properties.Settings.Default.Save();
        }

        private void UpdateLogInList(string currentUsername, string currentGroup)
        {
            Properties.Settings.Default.LogInUsername = currentUsername;
            Properties.Settings.Default.LogInGroup = currentGroup;
        }

        private void UpdateRecentUsernameList(string username)
        {
            Properties.Settings.Default.PastUsernameSecond = Properties.Settings.Default.PastUsernameFirst;
            Properties.Settings.Default.PastUsernameFirst = username;
        }

        private void UpdateRecentGroupList(string group)
        {
            Properties.Settings.Default.PastGroupSecond = Properties.Settings.Default.PastGroupFirst;
            Properties.Settings.Default.PastGroupFirst = group;
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