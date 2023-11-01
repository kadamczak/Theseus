using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands
{
    public class ShowDetailsCommand : CommandBase
    {
        private readonly MazeWithSolutionCommandViewModel _mazeWithSolutionCommandViewModel;
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        public ShowDetailsCommand(MazeWithSolutionCommandViewModel mazeWithSolutionCommandViewModel,
                                  SelectedMazeDetailsStore mazeDetailsStore,
                                  NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            this._mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailNavigationService = mazeDetailNavigationService;
        }

        public override void Execute(object? parameter)
        {
            MazeWithSolution mazeWithSolution = _mazeWithSolutionCommandViewModel.MazeWithSolutionCanvasViewModel.MazeWithSolution;
            _mazeDetailsStore.UpdateMazeDetails(mazeWithSolution, unsavedChanges: false);
            _mazeDetailNavigationService.Navigate();
        }
    }
}