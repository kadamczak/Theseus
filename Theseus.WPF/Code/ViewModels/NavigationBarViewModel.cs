﻿using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands.NavigationCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication.PatientAuthentication;
using Theseus.WPF.Code.Stores.Authentication.StaffMemberAuthentication;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    /// <summary>
    /// The <c>NavigationBarViewModel</c> class contains bindings for Navigation Bar.
    /// </summary>
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly ICurrentStaffMemberStore _currentStaffMemberStore;
        private readonly ICurrentPatientStore _currentPatientStore;

        public ICommand NavigateToBeginTest { get; }
        public ICommand NavigateToViewData { get; }
        public ICommand NavigateToCreateMaze { get; }
        public ICommand NavigateToCreateSet { get; }
        public ICommand NavigateToBrowseMazes { get; }
        public ICommand NavigateToBrowseSets { get; }

        public ICommand NavigateToSettings { get; }
        public ICommand NavigateToHome { get; }
        public ICommand OpenAccount { get; }

        public bool _loggedIn = false;
        public bool LoggedIn
        {
            get => _loggedIn;
            set
            {
                _loggedIn = value;
                OnPropertyChanged(nameof(LoggedIn));
            }
        }

        private bool _loggedInAsStaff = false;
        public bool LoggedInAsStaff
        {
            get => _loggedInAsStaff;
            set
            {
                _loggedInAsStaff = value;
                OnPropertyChanged(nameof(LoggedInAsStaff));
            }
        }

        private readonly NavigationEnabledStore _navigationEnabledStore;

        private bool _navigationEnabled = true;
        public bool NavigationEnabled
        {
            get => _navigationEnabled;
            set
            {
                _navigationEnabled = value;
                OnPropertyChanged(nameof(NavigationEnabled));
            }
        }


        public NavigationBarViewModel(NavigationService<BeginTestViewModel> beginTestNavigationService,
                                      NavigationService<ViewDataViewModel> viewDataNavigationService,
                                      NavigationService<CreateMazeViewModel> createMazeNavigationService,
                                      NavigationService<CreateSetViewModel> createSetNavigationService,
                                      NavigationService<BrowseMazesViewModel> browseMazesNavigationService,
                                      NavigationService<BrowseSetsViewModel> browseSetsNavigationService,
                                      NavigationService<SettingsViewModel> settingsNavigationService,
                                      NavigationService<HomeViewModel> homeNavigationService,
                                      NavigationService<LoggedInViewModel> loggedInNavigationService,
                                      NavigationService<NotLoggedInViewModel> notLoggedInNavigationService,
                                      NavigationEnabledStore navigationEnabledStore,
                                      ICurrentStaffMemberStore currentStaffMemberStore,
                                      ICurrentPatientStore currentPatientStore)
        {
            _navigationEnabledStore = navigationEnabledStore;
            _navigationEnabledStore.NavigationEnabledChanged += OnNavigationEnabledChanged;

            NavigateToBeginTest = new NavigateCommand<BeginTestViewModel>(beginTestNavigationService);
            NavigateToViewData = new NavigateCommand<ViewDataViewModel>(viewDataNavigationService);
            NavigateToCreateMaze = new NavigateCommand<CreateMazeViewModel>(createMazeNavigationService);
            NavigateToCreateSet = new NavigateCommand<CreateSetViewModel>(createSetNavigationService);
            NavigateToBrowseMazes = new NavigateCommand<BrowseMazesViewModel>(browseMazesNavigationService);
            NavigateToBrowseSets = new NavigateCommand<BrowseSetsViewModel>(browseSetsNavigationService);

            NavigateToSettings = new NavigateCommand<SettingsViewModel>(settingsNavigationService);
            NavigateToHome = new NavigateCommand<HomeViewModel>(homeNavigationService);

            OpenAccount = new OpenAccountViewModelCommand(loggedInNavigationService,
                                                          notLoggedInNavigationService,
                                                          currentPatientStore,
                                                          currentStaffMemberStore);

            this._currentStaffMemberStore = currentStaffMemberStore;
            this._currentPatientStore = currentPatientStore;
            this._currentStaffMemberStore.StaffMemberStateChanged += CurrentUserStateChanged;
            this._currentPatientStore.PatientStateChanged += CurrentUserStateChanged;
        }

        private void OnNavigationEnabledChanged()
        {
            NavigationEnabled = _navigationEnabledStore.NavigationEnabled;
        }

        private void CurrentUserStateChanged()
        {
            LoggedInAsStaff = _currentStaffMemberStore.IsStaffMemberLoggedIn;
            LoggedIn = _currentStaffMemberStore.IsStaffMemberLoggedIn || _currentPatientStore.IsPatientLoggedIn;
        }
    }
}