using System;
using System.Collections.ObjectModel;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        private readonly IGetAllMazesWithSolutionQuery _getMazeWithSolutionByIdQuery;
        public ObservableCollection<MazeWithSolutionCanvasViewModel> MazesWithSolution { get; } = new ObservableCollection<MazeWithSolutionCanvasViewModel>();

        public BrowseMazesViewModel(IGetAllMazesWithSolutionQuery getAllMazesWithSolutionQuery)
        {
            var mazesWithSolution = getAllMazesWithSolutionQuery.GetAllMazesWithSolution();

            foreach(var maze in mazesWithSolution)
            {
                MazesWithSolution.Add(new MazeWithSolutionCanvasViewModel(maze));
            }
        }
    }
}