using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsMazeCommandListViewModel : MazeCommandListViewModel
    {
        private readonly SelectedMazeDetailsStore _mazeDetailsStore;
        private readonly NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;

        public ShowDetailsMazeCommandListViewModel(SelectedModelListStore<MazeWithSolution> mazeListStore,
                                                   SelectedMazeDetailsStore mazeDetailsStore,
                                                   NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService) : base(mazeListStore)
        {
            _mazeDetailsStore = mazeDetailsStore;
            _mazeDetailsNavigationService = mazeDetailsNavigationService;
        }

        protected override void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution)
        {
            var actionableMaze = new CommandViewModel<MazeWithSolutionCanvasViewModel>(new MazeWithSolutionCanvasViewModel(mazeWithSolution))
            {
                Command1Name = "Details",
                ShowCommand1 = true,
            };

            actionableMaze.Command1 = new ShowMazeDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
            this.ActionableMazes.Add(actionableMaze);
        }
    }
}
