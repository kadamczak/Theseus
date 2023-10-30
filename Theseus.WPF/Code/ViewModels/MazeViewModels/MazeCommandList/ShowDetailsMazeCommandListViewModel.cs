using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsMazeCommandListViewModel : MazeCommandListViewModel
    {
        private MazeDetailsStore _mazeDetailsStore;
        private NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;


        public ShowDetailsMazeCommandListViewModel(MazeListStore mazeListStore,
                                                   MazeDetailsStore mazeDetailsStore,
                                                   NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService) : base(mazeListStore)
        {
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailsNavigationService = mazeDetailsNavigationService;
        }

        protected override void AddMazeWithCommandToActionableMazes(MazeWithSolution mazeWithSolution)
        {
            MazeWithSolutionCommandViewModel actionableMaze = new MazeWithSolutionCommandViewModel(mazeWithSolution);
            actionableMaze.Command = new ShowDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
            actionableMaze.CommandName = "Details";
            this.ActionableMazes.Add(actionableMaze);
        }
    }
}