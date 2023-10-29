using System.Windows.Input;
using Theseus.Domain.Models.MazeRelated.MazeRepresentation;

namespace Theseus.WPF.Code.ViewModels.Bindings
{
    public class MazeWithSolutionCommandViewModel
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }
        public ICommand Command { get; set; }

        public MazeWithSolutionCommandViewModel(MazeWithSolution mazeWithSolution)
        {
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
        }
    }
}
