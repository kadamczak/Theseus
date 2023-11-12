using Theseus.Domain.QueryInterfaces.MazeQueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores.Mazes;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public ShowDetailsMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public BrowseMazesViewModel(SelectedMazeListStore mazeListStore,
                                    IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                    ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            LoadFullMazeList(getAllMazesWithSolutionQuery, mazeListStore);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeList(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery, SelectedMazeListStore mazeListStore)
        {
            var fullMazeList = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}