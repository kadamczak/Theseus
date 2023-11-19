using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands.MazeCommands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores.Mazes;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsMazeCommandListViewModel : MazeCommandListViewModel
    {
        private SelectedMazeDetailsStore _mazeDetailsStore;
        private NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;


        public ShowDetailsMazeCommandListViewModel(SelectedMazeListStore mazeListStore,
                                                   SelectedMazeDetailsStore mazeDetailsStore,
                                                   NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService) : base(mazeListStore)
        {
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailsNavigationService = mazeDetailsNavigationService;
        }

        protected override void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution)
        {
            MazeWithSolutionCommandViewModel actionableMaze = new MazeWithSolutionCommandViewModel(mazeWithSolution);
            actionableMaze.Command = new ShowMazeDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
            actionableMaze.CommandName = "Details";
            this.ActionableMazes.Add(actionableMaze);
        }
    }
}