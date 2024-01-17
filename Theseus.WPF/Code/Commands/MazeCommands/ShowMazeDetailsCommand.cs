using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels;
using Theseus.WPF.Code.ViewModels.Bindings.CommandViewModel;

namespace Theseus.WPF.Code.Commands.MazeCommands
{
    /// <summary>
    /// The <c>ShowMazeDetailsCommand</c> class reads the <c>MazeWithSolution</c> object stored in linked <c>CommandViewModel</c> instance,
    /// saves it in <c>SelectedModelDetailsStore</c> and then opens the Maze Details View.
    /// </summary>
    public class ShowMazeDetailsCommand : CommandBase
    {
        private readonly CommandViewModel<MazeWithSolutionCanvasViewModel> _mazeWithSolutionCommandViewModel;
        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailNavigationService;

        public ShowMazeDetailsCommand(CommandViewModel<MazeWithSolutionCanvasViewModel> mazeWithSolutionCommandViewModel,
                                  SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
                                  NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            _mazeWithSolutionCommandViewModel = mazeWithSolutionCommandViewModel;
            _mazeDetailsStore = mazeDetailsStore;
            _mazeDetailNavigationService = mazeDetailNavigationService;
        }

        public override void Execute(object? parameter)
        {
            _mazeDetailsStore.SelectedModel = _mazeWithSolutionCommandViewModel.Model;
            _mazeDetailNavigationService.Navigate();
        }
    }
}