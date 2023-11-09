using System;
using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Authentication;

namespace Theseus.WPF.Code.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly ICurrentUserStore _currentUserStore;

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
                                      ICurrentUserStore currentUserStore)
        {
            NavigateToBeginTest = new NavigateCommand<BeginTestViewModel>(beginTestNavigationService);
            NavigateToViewData = new NavigateCommand<ViewDataViewModel>(viewDataNavigationService);
            NavigateToCreateMaze = new NavigateCommand<CreateMazeViewModel>(createMazeNavigationService);
            NavigateToCreateSet = new NavigateCommand<CreateSetViewModel>(createSetNavigationService);
            NavigateToBrowseMazes = new NavigateCommand<BrowseMazesViewModel>(browseMazesNavigationService);
            NavigateToBrowseSets = new NavigateCommand<BrowseSetsViewModel>(browseSetsNavigationService);

            NavigateToSettings = new NavigateCommand<SettingsViewModel>(settingsNavigationService);
            NavigateToHome = new NavigateCommand<HomeViewModel>(homeNavigationService);
            OpenAccount = new OpenAccountViewModelCommand(loggedInNavigationService, notLoggedInNavigationService, currentUserStore);

            this._currentUserStore = currentUserStore;
            this._currentUserStore.StaffMemberStateChanged += StaffMemberStateChanged;
        }

        private void StaffMemberStateChanged()
        {
            if(_currentUserStore.CurrentStaffMember is null)
            {
                LoggedInAsStaff = false;
            }
            else
            {
                LoggedIn = true;
                LoggedInAsStaff = true;
            }
        }

    }
}
