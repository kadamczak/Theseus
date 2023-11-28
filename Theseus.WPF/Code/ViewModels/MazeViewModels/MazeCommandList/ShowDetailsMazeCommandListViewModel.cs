using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsMazeCommandListViewModel : CommandListViewModel<MazeWithSolutionCanvasViewModel>
    {
        private readonly SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;

        public ShowDetailsMazeCommandListViewModel(SelectedModelListStore<MazeWithSolutionCanvasViewModel> mazeListStore,
                                                   SelectedModelDetailsStore<MazeWithSolutionCanvasViewModel> mazeDetailsStore,
                                                   NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService) : base(mazeListStore)
        {
            _mazeDetailsStore = mazeDetailsStore;
            _mazeDetailsNavigationService = mazeDetailsNavigationService;
        }

        public override void AddModelToActionableModels(MazeWithSolutionCanvasViewModel mazeWithSolution)
        {
            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(mazeWithSolution)
            {
                Command1Name = "Details",
                ShowCommand1 = true,
            };

            actionableMaze.Command1 = new ShowMazeDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
            this.ActionableModels.Add(actionableMaze);
        }
    }
}