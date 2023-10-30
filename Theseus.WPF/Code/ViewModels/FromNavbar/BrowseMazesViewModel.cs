using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public ShowDetailsMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }

        public BrowseMazesViewModel(MazeListStore mazeListStore,
                                    IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                    ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            LoadFullMazeList(getAllMazesWithSolutionQuery, mazeListStore);

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeList(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery, MazeListStore mazeListStore)
        {
            var fullMazeList = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();
            mazeListStore.MazesInList = fullMazeList;
        }
    }
}