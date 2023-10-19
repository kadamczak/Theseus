using Theseus.Domain.Models.MazeRelated.MazeRepresentation;
using Theseus.WPF.Code.Bases;

namespace Theseus.WPF.Code.ViewModels.Components
{
    public class ExamMazeCanvasViewModel : ViewModelBase
    {
        public MazeWithSolutionCanvasViewModel MazeWithSolutionCanvasViewModel { get; }

        public ExamMazeCanvasViewModel(MazeWithSolution mazeWithSolution)
        {
            this.MazeWithSolutionCanvasViewModel = new MazeWithSolutionCanvasViewModel(mazeWithSolution);
        }
    }
}