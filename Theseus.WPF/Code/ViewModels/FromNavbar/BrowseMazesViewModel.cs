using System.Collections.ObjectModel;
using System.Windows.Input;
using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;
using Theseus.WPF.Code.Commands;
using Theseus.WPF.Code.Services;
using Theseus.WPF.Code.Stores;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        private readonly IGetAllMazesWithSolutionQuery _getMazeWithSolutionByIdQuery;
        public ObservableCollection<MazeWithSolutionCanvasViewModel> MazesWithSolution { get; } = new ObservableCollection<MazeWithSolutionCanvasViewModel>();

        public BrowseMazesViewModel(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery,
                                    MazeDetailsStore mazeDetailsStore,
                                    NavigationService<MazeDetailsViewModel> mazeDetailNavigationService)
        {
            var mazesWithSolution = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();

            //ShowDetails = new ShowDetailsCommand(mazeDetailsStore, mazeDetailNavigationService);

            foreach(var maze in mazesWithSolution)
            {
                MazesWithSolution.Add(new MazeWithSolutionCanvasViewModel(maze));
            }
        }
    }
}