using System.Windows.Input;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;

namespace Theseus.WPF.Code.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateToHome { get; }
        public ICommand NavigateToMazeGenSettings { get; }

        public NavigationBarViewModel(NavigationService<HomeViewModel> homeNavigationService, NavigationService<MazeGeneratorViewModel> mazeGenNavigationService)
        {
            NavigateToHome = new NavigateCommand<HomeViewModel>(homeNavigationService);
            NavigateToMazeGenSettings = new NavigateCommand<MazeGeneratorViewModel>(mazeGenNavigationService);
        }
    }
}
