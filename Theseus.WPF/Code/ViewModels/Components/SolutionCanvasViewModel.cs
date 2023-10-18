using System.Collections.Generic;
using Theseus.Domain.Models.MazeRelated.Enums;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class SolutionCanvasViewModel : ViewModelBase
    {
        public List<Cell> SolutionPath { get; }
        public Direction StartDirection { get; }
        public Direction EndDirection { get; }

        public SolutionCanvasViewModel(MazeWithSolution mazeWithSolution)
        {
            SolutionPath = mazeWithSolution.SolutionPath;
            StartDirection = mazeWithSolution.StartDirection;
            EndDirection = mazeWithSolution.EndDirection;
        }
    }
}
