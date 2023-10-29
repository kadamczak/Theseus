using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;
using Theseus.WPF.Code.ViewModels.Bindings;

namespace Theseus.WPF.Code.ViewModels
{
    public class ShowDetailsMazeCommandListViewModel : MazeCommandListViewModel
    {
        private MazeListStore _mazeListStore;
        private MazeDetailsStore _mazeDetailsStore;
        private NavigationService<MazeDetailsViewModel> _mazeDetailsNavigationService;


        public ShowDetailsMazeCommandListViewModel(MazeListStore mazeListStore,
                                                   MazeDetailsStore mazeDetailsStore,
                                                   NavigationService<MazeDetailsViewModel> mazeDetailsNavigationService) : base()
        {
            this._mazeListStore = mazeListStore;
            this._mazeDetailsStore = mazeDetailsStore;
            this._mazeDetailsNavigationService = mazeDetailsNavigationService;
            LoadMazesFromMazeListStore();
        }

        public void LoadMazesFromMazeListStore()
        {
            this.ActionableMazes.Clear();

            foreach (var mazeWithSolution in _mazeListStore.MazesInList)
            {
                AddMazeWithCommandToList(mazeWithSolution);
            }
        }

        private void AddMazeWithCommandToList(MazeWithSolution mazeWithSolution)
        {
            MazeWithSolutionCommandViewModel actionableMaze = new MazeWithSolutionCommandViewModel(mazeWithSolution);
            actionableMaze.Command = new ShowDetailsCommand(actionableMaze, _mazeDetailsStore, _mazeDetailsNavigationService);
            this.ActionableMazes.Add(actionableMaze);
        }
    }
}