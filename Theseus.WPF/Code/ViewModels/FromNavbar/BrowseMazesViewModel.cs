using System;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.Domain.QueryInterfaces;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class BrowseMazesViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        private readonly IGetMazeWithSolutionByIdQuery _getMazeWithSolutionByIdQuery;

        public BrowseMazesViewModel(IGetMazeWithSolutionByIdQuery getMazeWithSolutionByIdQuery)
        {
            Guid id = new Guid("b879799d-af75-4ce4-734d-08dbd14fb5c2");
            MazeWithSolution maze = getMazeWithSolutionByIdQuery.GetMazeWithSolutionById(id);       
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(maze);
        }
    }
}