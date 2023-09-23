using System.Windows.Input;
using Theseus.Code.Bases;
using Theseus.Code.Commands;
using Theseus.Code.Services;

namespace Theseus.Code.MVVM.ViewModels
{
    public class NavigationBarViewModel : ViewModel
    {
        public ICommand NavigateToHome { get; }
        public ICommand NavigateToMazeGenSettings { get; }

        public NavigationBarViewModel(NavigationService homeNavigationService, NavigationService mazeGenSettingsNavigationService)
        {
            NavigateToHome = new NavigateCommand(homeNavigationService);
            NavigateToMazeGenSettings = new NavigateCommand(mazeGenSettingsNavigationService);
        }

    }
}
