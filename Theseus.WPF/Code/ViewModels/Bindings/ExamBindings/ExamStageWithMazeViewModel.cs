using Theseus.Domain.Models.ExamRelated;

namespace Theseus.WPF.Code.ViewModels.Bindings.ExamBindings
{
    public class ExamStageWithMazeViewModel
    {
        public ExamStage ExamStage { get; set; }
        public MazeWithSolutionCanvasViewModel MazeCanvasViewModel { get; set; }

        public ExamStageWithMazeViewModel(ExamStage examStage, MazeWithSolutionCanvasViewModel mazeCanvasViewModel)
        {
            ExamStage = examStage;
            MazeCanvasViewModel = mazeCanvasViewModel;
        }
    }
}