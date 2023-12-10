using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels
{
    public class MazeWithSolutionCanvasViewModel : ViewModelBase
    {
        public MazeCanvasViewModel MazeCanvasViewModel { get; }
        public SolutionCanvasViewModel SolutionCanvasViewModel { get; }
        public MazeWithSolution MazeWithSolution { get; }

        public MazeWithSolutionCanvasViewModel(MazeWithSolution mazeWithSolution)
        {
            this.MazeWithSolution = mazeWithSolution;
            this.MazeCanvasViewModel = new MazeCanvasViewModel(mazeWithSolution.Grid);
            this.SolutionCanvasViewModel = new SolutionCanvasViewModel(mazeWithSolution);
        }
    }
}