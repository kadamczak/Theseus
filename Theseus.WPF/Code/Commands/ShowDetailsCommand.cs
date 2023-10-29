using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;

namespace Theseus.WPF.Code.Commands
{
    public class ShowDetailsCommand : CommandBase
    {
        private readonly MazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        public ShowDetailsCommand(MazeDetailsStore mazeDetailsStore, NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailNavigationService = mazeDetailNavigationService;
        }

        public override void Execute(object? parameter)
        {
            //_mazeDetailsStore.UpdateMazeDetails(mazeWithSolution, unsavedChanges: false);
            _mazeDetailNavigationService.Navigate();
        }
    }
}
