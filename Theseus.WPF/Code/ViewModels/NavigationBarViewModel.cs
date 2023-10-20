using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateToBeginTest { get; }
        public ICommand NavigateToViewData { get; }
        public ICommand NavigateToCreateMaze { get; }
        public ICommand NavigateToCreateSet { get; }
        public ICommand NavigateToBrowseMazes { get; }
        public ICommand NavigateToBrowseSets { get; }

        public ICommand NavigateToSettings { get; }
        public ICommand NavigateToHome { get; }

        public NavigationBarViewModel(NavigationService<BeginTestViewModel> beginTestNavigationService,
                                      NavigationService<ViewDataViewModel> viewDataNavigationService,
                                      NavigationService<CreateMazeViewModel> createMazeNavigationService,
                                      NavigationService<CreateSetViewModel> createSetNavigationService,
                                      NavigationService<BrowseMazesViewModel> browseMazesNavigationService,
                                      NavigationService<BrowseSetsViewModel> browseSetsNavigationService,
                                      NavigationService<SettingsViewModel> settingsNavigationService,
                                      NavigationService<HomeViewModel> homeNavigationService)
        {
            NavigateToBeginTest = new NavigateCommand<BeginTestViewModel>(beginTestNavigationService);
            NavigateToViewData = new NavigateCommand<ViewDataViewModel>(viewDataNavigationService);
            NavigateToCreateMaze = new NavigateCommand<CreateMazeViewModel>(createMazeNavigationService);
            NavigateToCreateSet = new NavigateCommand<CreateSetViewModel>(createSetNavigationService);
            NavigateToBrowseMazes = new NavigateCommand<BrowseMazesViewModel>(browseMazesNavigationService);
            NavigateToBrowseSets = new NavigateCommand<BrowseSetsViewModel>(browseSetsNavigationService);

            NavigateToSettings = new NavigateCommand<SettingsViewModel>(settingsNavigationService);
            NavigateToHome = new NavigateCommand<HomeViewModel>(homeNavigationService);
        }
    }
}
