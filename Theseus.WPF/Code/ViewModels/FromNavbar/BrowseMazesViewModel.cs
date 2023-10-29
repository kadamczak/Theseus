using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public ShowDetailsMazeCommandListViewModel ShowDetailsMazeCommandViewModel { get; }
        private readonly MazeListStore _mazeListStore;
        private readonly IGetAllMazesWithSolutionQuery _getAllMazesWithSolutionQuery;

        public BrowseMazesViewModel(MazeListStore mazeListStore,
                                    IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                    ShowDetailsMazeCommandListViewModel showDetailsMazeCommandListViewModel)
        {
            this._mazeListStore = mazeListStore;
            this._getAllMazesWithSolutionQuery = getAllMazesWithSolutionQuery;

            LoadFullMazeList();

            this.ShowDetailsMazeCommandViewModel = showDetailsMazeCommandListViewModel;
            this.ShowDetailsMazeCommandViewModel.LoadMazesFromMazeListStore();
        }

        private void LoadFullMazeList()
        {
            var fullMazeList = _getAllMazesWithSolutionQuery.GetAllMazesWithSolution();
            _mazeListStore.MazesInList = fullMazeList;
        }
    }
}