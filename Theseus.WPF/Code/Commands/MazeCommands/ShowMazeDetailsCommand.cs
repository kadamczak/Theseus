using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    public class ShowMazeDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeWithSolutionCommandViewModel;
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        public ShowMazeDetailsCommand(CommandViewModel<MazeWithSolutionCanvasViewModel> mazeWithSolutionCommandViewModel,
                                  SelectedMazeDetailsStore mazeDetailsStore,
                                  NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            _mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            _mazeDetailsStore = mazeDetailsStore;
            _mazeDetailNavigationService = mazeDetailNavigationService;
        }

        public override void Execute(object? parameter)
        {
            MazeWithSolution mazeWithSolution = _mazeWithSolutionCommandViewModel.Model.MazeWithSolution;
            _mazeDetailsStore.UpdateMazeDetails(mazeWithSolution, unsavedChanges: false);
            _mazeDetailNavigationService.Navigate();
        }
    }
}